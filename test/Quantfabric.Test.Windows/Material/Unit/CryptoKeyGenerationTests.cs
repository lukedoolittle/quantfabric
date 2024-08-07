﻿using Material.HttpClient.Cryptography;
using Material.HttpClient.Cryptography.Enums;
using Material.HttpClient.Cryptography.Keys;
using Quantfabric.Test.Helpers;
using Xunit;

namespace Foundations.Test.HttpClient
{
    [Trait("Category", "Continuous")]
    public class CryptoKeyGenerationTests
    {
        private readonly IJsonWebTokenSigningFactory _factory =
            new JsonWebTokenSignerFactory();
        private readonly Randomizer _randomizer =
            new Randomizer();

        [Fact]
        public void GenerateAndVerifyRs256JsonWebToken()
        {
            var keyPair = RsaCryptoKeyPair.Create(1024);
            var plaintext = _randomizer.RandomString(20, 40);
            var algorithm = JsonWebTokenAlgorithm.RS256;

            var signer = _factory.GetSigningAlgorithm(algorithm);
            var verifier = _factory.GetVerificationAlgorithm(algorithm);

            var signature = signer.SignMessage(
                plaintext,
                keyPair.Private);

            var isVerified = verifier.VerifyMessage(
                keyPair.Public,
                signature,
                plaintext);

            Assert.True(isVerified);
        }

        [Fact]
        public void GenerateAndVerifyRs384JsonWebToken()
        {
            var keyPair = RsaCryptoKeyPair.Create(1024);
            var plaintext = _randomizer.RandomString(20, 40);
            var algorithm = JsonWebTokenAlgorithm.RS384;

            var signer = _factory.GetSigningAlgorithm(algorithm);
            var verifier = _factory.GetVerificationAlgorithm(algorithm);

            var signature = signer.SignMessage(
                plaintext,
                keyPair.Private);

            var isVerified = verifier.VerifyMessage(
                keyPair.Public,
                signature,
                plaintext);

            Assert.True(isVerified);
        }

        [Fact]
        public void GenerateAndVerifyRs512JsonWebToken()
        {
            var keyPair = RsaCryptoKeyPair.Create(1024);
            var plaintext = _randomizer.RandomString(20, 40);
            var algorithm = JsonWebTokenAlgorithm.RS384;

            var signer = _factory.GetSigningAlgorithm(algorithm);
            var verifier = _factory.GetVerificationAlgorithm(algorithm);

            var signature = signer.SignMessage(
                plaintext,
                keyPair.Private);

            var isVerified = verifier.VerifyMessage(
                keyPair.Public,
                signature,
                plaintext);

            Assert.True(isVerified);
        }

        [Fact]
        public void GenerateAndVerifyEs256JsonWebToken()
        {
            var curveName = "P-256";
            var keyPair = EcdsaCryptoKeyPair.Create(curveName);
            var plaintext = _randomizer.RandomString(20, 40);
            var algorithm = JsonWebTokenAlgorithm.ES256;

            var signer = _factory.GetSigningAlgorithm(algorithm);
            var verifier = _factory.GetVerificationAlgorithm(algorithm);

            var signature = signer.SignMessage(
                plaintext,
                keyPair.Private);

            var isVerified = verifier.VerifyMessage(
                keyPair.Public,
                signature,
                plaintext);

            Assert.True(isVerified);
        }

        [Fact]
        public void GenerateAndVerifyEs384JsonWebToken()
        {
            var curveName = "P-384";
            var keyPair = EcdsaCryptoKeyPair.Create(curveName);
            var plaintext = _randomizer.RandomString(20, 40);
            var algorithm = JsonWebTokenAlgorithm.ES384;

            var signer = _factory.GetSigningAlgorithm(algorithm);
            var verifier = _factory.GetVerificationAlgorithm(algorithm);

            var signature = signer.SignMessage(
                plaintext,
                keyPair.Private);

            var isVerified = verifier.VerifyMessage(
                keyPair.Public,
                signature,
                plaintext);

            Assert.True(isVerified);
        }

        [Fact]
        public void GenerateAndVerifyEs512JsonWebToken()
        {
            var curveName = "P-521";
            var keyPair = EcdsaCryptoKeyPair.Create(curveName);
            var plaintext = _randomizer.RandomString(20, 40);
            var algorithm = JsonWebTokenAlgorithm.ES512;

            var signer = _factory.GetSigningAlgorithm(algorithm);
            var verifier = _factory.GetVerificationAlgorithm(algorithm);

            var signature = signer.SignMessage(
                plaintext,
                keyPair.Private);

            var isVerified = verifier.VerifyMessage(
                keyPair.Public,
                signature,
                plaintext);

            Assert.True(isVerified);
        }

        [Fact]
        public void GenerateAndVerifyHs256JsonWebToken()
        {
            var key = _randomizer.RandomString(200);
            var plaintext = _randomizer.RandomString(20, 40);
            var algorithm = JsonWebTokenAlgorithm.HS256;

            var signer = _factory.GetSigningAlgorithm(algorithm);
            var verifier = _factory.GetVerificationAlgorithm(algorithm);

            var signature = signer.SignMessage(
                plaintext,
                new HashKey(key, StringEncoding.Utf8));

            var isVerified = verifier.VerifyMessage(
                new HashKey(key, StringEncoding.Utf8),
                signature,
                plaintext);

            Assert.True(isVerified);
        }

        public void GenerateAndVerifyHs384JsonWebToken()
        {
            var key = _randomizer.RandomString(200);
            var plaintext = _randomizer.RandomString(20, 40);
            var algorithm = JsonWebTokenAlgorithm.HS384;

            var signer = _factory.GetSigningAlgorithm(algorithm);
            var verifier = _factory.GetVerificationAlgorithm(algorithm);

            var signature = signer.SignMessage(
                plaintext,
                new HashKey(key, StringEncoding.Utf8));

            var isVerified = verifier.VerifyMessage(
                new HashKey(key, StringEncoding.Utf8),
                signature,
                plaintext);

            Assert.True(isVerified);
        }

        public void GenerateAndVerifyHs512JsonWebToken()
        {
            var key = _randomizer.RandomString(200);
            var plaintext = _randomizer.RandomString(20, 40);
            var algorithm = JsonWebTokenAlgorithm.HS512;

            var signer = _factory.GetSigningAlgorithm(algorithm);
            var verifier = _factory.GetVerificationAlgorithm(algorithm);

            var signature = signer.SignMessage(
                plaintext,
                new HashKey(key, StringEncoding.Utf8));

            var isVerified = verifier.VerifyMessage(
                new HashKey(key, StringEncoding.Utf8),
                signature,
                plaintext);

            Assert.True(isVerified);
        }
    }
}
