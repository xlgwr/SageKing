using AutoFixture;
using Moq;
using SageKing.Core.Contracts;
using SageKing.IceRPC.Contracts;
using Xunit.Abstractions;

namespace SageKing.IceRPC.Test
{
    public class SageKingMessage_Test(ISageKingMessage sageKingMessage, ISageKingMessage sageKingMessageLoad, IFixture _fixture)
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

        [Fact]
        public void toData_loadData()
        {
            //Arrange 
            var attributename = _fixture.Create<string>();

            int step = 0;
            for (int i = 0; i < 3; i++)
            {
                var valuestring = new DataStreamTypValue<string>(_fixture.Create<string>());
                var valueInt = new DataStreamTypValue<int>(_fixture.Create<int>());
                var valuebyte = new DataStreamTypValue<byte>(_fixture.Create<byte>());
                var valuesbyte = new DataStreamTypValue<sbyte>(_fixture.Create<sbyte>());
                var valuefloat = new DataStreamTypValue<float>(_fixture.Create<float>());
                var valuelong = new DataStreamTypValue<long>(_fixture.Create<long>());
                var valuedouble = new DataStreamTypValue<double>(_fixture.Create<double>());

                //Act
                sageKingMessage.AddOrUpdate($"{attributename}_{step}", valuestring);
                sageKingMessage.AddOrUpdate($"{attributename}_{step + 1}", valueInt);
                sageKingMessage.AddOrUpdate($"{attributename}_{step + 2}", valuebyte);
                sageKingMessage.AddOrUpdate($"{attributename}_s_{step + 2}", valuesbyte);
                sageKingMessage.AddOrUpdate($"{attributename}_{step + 3}", valuefloat);
                sageKingMessage.AddOrUpdate($"{attributename}_{step + 4}", valuelong);
                sageKingMessage.AddOrUpdate($"{attributename}_{step + 5}", valuedouble);

                step += 8;
            }

            //”√”⁄≤‚ ‘load
            var attributenameLoad = _fixture.Create<string>();
            sageKingMessage.AddOrUpdate($"{attributenameLoad}", new DataStreamTypValue<string>(attributenameLoad));

            //Act

            //test todata
            var result = sageKingMessage.ToData();

            //test loaddata
            sageKingMessageLoad.InitAttribytePos(sageKingMessage.GetPosData());

            sageKingMessageLoad.LoadData(result);

            var getLoaddata = sageKingMessageLoad.Get(attributenameLoad);

            //Assert
            Assert.Equal(result.RowType?.Length, 7);
            Assert.Equal(result.DataBody?.Length, 7);

            Assert.Equal(getLoaddata, attributenameLoad);

        }
    }
}