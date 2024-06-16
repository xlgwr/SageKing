using AutoFixture;
using Moq;
using SageKing.Core.Contracts;
using SageKing.IceRPC.Contracts;
using Xunit.Abstractions;

namespace SageKing.IceRPC.Test
{
    public class SageKingMessage_Test(ISageKingMessage sageKingMessage, IFixture _fixture)
    {
        [Fact]
        public void AddOrUpdate_Get_remove_string()
        {
            //Arrange 
            var attributename = _fixture.Create<string>();
            var value = new DataStreamTypValue<string>(DataStreamTypeEnum.String, attributename);

            //Act
            sageKingMessage.AddOrUpdate(attributename, value);

            var getValue = sageKingMessage.Get(attributename);

            sageKingMessage.Remove(attributename, value.type);

            var getValue2 = sageKingMessage.Get(attributename);

            //Assert
            Assert.Equal(getValue, value.value);

            Assert.Equal(getValue2, string.Empty);


        }
    }
}