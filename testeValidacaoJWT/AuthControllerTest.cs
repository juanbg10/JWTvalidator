using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using validacaoJWT.Controllers;
using validacaoJWT.Model;
using validacaoJWT.Profiles;
using validacaoJWT.Services;

namespace testeValidacaoJWT

{
    public class AuthControllerTest
    {
        private readonly AuthController _controller;
        private readonly Mock<ILogger<AuthController>> _logger;
        private readonly Mock<ITokenService> _service;
        private readonly IMapper _mapper;
        private readonly Exception _exception;
        private readonly ArgumentException _argumentException;

        public AuthControllerTest()
        {
            var mapperProfiles = new MapperProfiles();

            var configuration = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(mapperProfiles);
            });

            var _mapper = new Mapper(configuration);

            _logger = new Mock<ILogger<AuthController>>();
            _service = new Mock<ITokenService>();
            _controller = new AuthController(_service.Object, _mapper, _logger.Object);
        }

        #region Teste de instancia da classe
        [Fact]
        public void InstanciaErrologger()
        {
            try
            {
                var instancia = new AuthController(_service.Object, _mapper, null);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentNullException>(e);
                Assert.Equal("Value cannot be null. (Parameter 'logger')", e.Message);
            }
        }

        [Fact]
        public void InstanciaErroService()
        {
            try
            {
                var instancia = new AuthController(null, _mapper, _logger.Object);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentNullException>(e);
                Assert.Equal("Value cannot be null. (Parameter 'service')", e.Message);
            }
        }

        [Fact]
        public void InstanciaErroMapper()
        {
            try
            {
                var instancia = new AuthController(_service.Object, null, _logger.Object);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentNullException>(e);
                Assert.Equal("Value cannot be null. (Parameter 'mapper')", e.Message);
            }
        }
        #endregion

        #region Entradas da DOC
        [Fact]
        [Trait("ValidateJwt", "Token verdadeiro")]
        public void ValidateJwtVerdareiro()
        {
            var token = new JwtDto()
            {
                jwt = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJTZWVkIjoiNzg0MSIsIk5hbWUiOiJUb25pbmhvIEFyYXVqbyJ9.QY05sIjtrcJnP533kQNk8QXcaleJ1Q01jWY_ZzIZuAg"
            };

            var resultado = _controller.ValidateJwt(token);

            Assert.True(resultado.Value);
        }

        [Fact]
        [Trait("ValidateJwt", "JWT invalido")]
        public void ValidateJwtInvalido()
        {
            var token = new JwtDto()
            {
                jwt = "eyJhbGciOiJzI1NiJ9.dfsdfsfryJSr2xrIjoiQWRtaW4iLCJTZrkIjoiNzg0MSIsIk5hbrUiOiJUb25pbmhvIEFyYXVqbyJ9.QY05fsdfsIjtrcJnP533kQNk8QXcaleJ1Q01jWY_ZzIZuAg"
            };

            var resultado = _controller.ValidateJwt(token);

            Assert.False(resultado.Value);
        }

        [Fact]
        [Trait("ValidateJwt", "Abrindo o JWT, a Claim Name possui caracter de n�meros")]
        public void ValidateJwtInvalidoClaimNumero()
        {
            var token = new JwtDto()
            {
                jwt = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiRXh0ZXJuYWwiLCJTZWVkIjoiODgwMzciLCJOYW1lIjoiTTRyaWEgT2xpdmlhIn0.6YD73XWZYQSSMDf6H0i3-kylz1-TY_Yt6h1cV2Ku-Qs"
            };

            var resultado = _controller.ValidateJwt(token);

            Assert.False(resultado.Value);
        }

        [Fact]
        [Trait("ValidateJwt", "Abrindo o JWT, foi encontrado mais de 3 claims")]
        public void ValidateJwtInvalidoClaims4()
        {
            var token = new JwtDto()
            {
                jwt = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiTWVtYmVyIiwiT3JnIjoiQlIiLCJTZWVkIjoiMTQ2MjciLCJOYW1lIjoiVmFsZGlyIEFyYW5oYSJ9.cmrXV_Flm5mfdpfNUVopY_I2zeJUy4EZ4i3Fea98zvY"
            };

            var resultado = _controller.ValidateJwt(token);

            Assert.False(resultado.Value);
        }
        #endregion
    }
}