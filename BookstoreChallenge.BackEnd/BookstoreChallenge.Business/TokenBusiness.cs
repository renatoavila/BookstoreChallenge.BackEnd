using BookstoreChallenge.Business.Interface;
using BookstoreChallenge.Domain;
using BookstoreChallenge.Domain.Entity;
using BookstoreChallenge.Repository.Interface;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using System;

namespace BookstoreChallenge.Business
{
    public sealed class TokenBusiness : ITokenBusiness
    {
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IUserRepository _userRepository;

        public TokenBusiness(TokenConfiguration tokenConfiguration, IUserRepository userRepository)
        {
            _tokenConfiguration = tokenConfiguration;
            _userRepository = userRepository;
        }

        public string GenerateToken(Users user)
        {
            var token = CreateToken(user, _tokenConfiguration);

            return token;
        }

        public bool Validate(string userName, string token, out string message)
        {
            try
            {
                var user = GetUser(userName);

                if (user == null)
                {
                    message = "User not found";
                    return false;
                }

                new JwtBuilder()
                    .WithSecret(user.SecretKey)
                    .MustVerifySignature()
                    .Decode(token);

                message = "Token is valid";

                return true;
            }
            catch (TokenExpiredException)
            {
                message = "Token has expired";

                return false;
            }
            catch (SignatureVerificationException)
            {
                message = "Token has invalid signature";

                return false;
            }
            catch (Exception exception)
            {
                message = exception.Message;

                return false;
            }
        }

        public string RefreshToken(string userName)
        {
            var user = GetUser(userName);
            var token = GenerateToken(user);

            return token;
        }

        private Users GetUser(string userName)
        {
            var user = _userRepository.GetByKey(userName);

            return user;
        }

        private static string CreateToken(Users user, TokenConfiguration tokenConfiguration)
        {
            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(user.SecretKey)
                .AddClaim("exp", DateTimeOffset.UtcNow
                    .AddSeconds(tokenConfiguration.ExpirationInSeconds).ToUnixTimeSeconds())
                .AddClaim("sub", user.UserName)
                .AddClaim("iss", tokenConfiguration.Issuer)
                .AddClaim("aud", tokenConfiguration.Audience)
                .Build();

            return token;
        }
    }
}