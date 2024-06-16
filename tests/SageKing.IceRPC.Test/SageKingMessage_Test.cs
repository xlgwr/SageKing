using AutoFixture;
using Moq;
using SageKing.Core.Contracts;
using SageKing.IceRPC.Contracts;
using Xunit.Abstractions;

namespace SageKing.IceRPC.Test
{
    public class SageKingMessage_Test(ISageKingMessage sageKingMessage, IFixture _fixture)
    {
        [Theory]
        [InlineData("strdemo")]
        public void AddOrUpdate_Get_remove_string(string tmpValue)
        {
            //Arrange 
            var attributename = _fixture.Create<string>();
            var value = new DataStreamTypValue<string>(tmpValue);

            //Act
            sageKingMessage.AddOrUpdate(attributename, value);

            var getValue = sageKingMessage.Get(attributename);

            sageKingMessage.Remove(attributename, value.DataStreamType);

            var getValue2 = sageKingMessage.Get(attributename);

            //Assert
            Assert.Equal(getValue, value.Value);

            Assert.Equal(getValue2, string.Empty);
        }

        [Theory]
        [InlineData(110)]
        public void AddOrUpdate_Get_remove_int(int tmpValue)
        {
            //Arrange 
            var attributename = _fixture.Create<string>();
            var value = new DataStreamTypValue<int>(tmpValue);

            //Act
            sageKingMessage.AddOrUpdate(attributename, value);

            var getValue = sageKingMessage.Getint(attributename);

            sageKingMessage.Remove(attributename, value.DataStreamType);

            var getValue2 = sageKingMessage.Getint(attributename);

            //Assert
            Assert.Equal(getValue, value.Value);

            Assert.Equal(getValue2, default(int));
        }
    }
}