
using Business.Handlers.ST_OgrenciDurums.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_OgrenciDurums.Queries.GetST_OgrenciDurumQuery;
using Entities.Concrete;
using static Business.Handlers.ST_OgrenciDurums.Queries.GetST_OgrenciDurumsQuery;
using static Business.Handlers.ST_OgrenciDurums.Commands.CreateST_OgrenciDurumCommand;
using Business.Handlers.ST_OgrenciDurums.Commands;
using Business.Constants;
using static Business.Handlers.ST_OgrenciDurums.Commands.UpdateST_OgrenciDurumCommand;
using static Business.Handlers.ST_OgrenciDurums.Commands.DeleteST_OgrenciDurumCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_OgrenciDurumHandlerTests
    {
        Mock<IST_OgrenciDurumRepository> _sT_OgrenciDurumRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_OgrenciDurumRepository = new Mock<IST_OgrenciDurumRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_OgrenciDurum_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_OgrenciDurumQuery();

            _sT_OgrenciDurumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgrenciDurum, bool>>>())).ReturnsAsync(new ST_OgrenciDurum()
//propertyler buraya yazılacak
//{																		
//ST_OgrenciDurumId = 1,
//ST_OgrenciDurumName = "Test"
//}
);

            var handler = new GetST_OgrenciDurumQueryHandler(_sT_OgrenciDurumRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_OgrenciDurumId.Should().Be(1);

        }

        [Test]
        public async Task ST_OgrenciDurum_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_OgrenciDurumsQuery();

            _sT_OgrenciDurumRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_OgrenciDurum, bool>>>()))
                        .ReturnsAsync(new List<ST_OgrenciDurum> { new ST_OgrenciDurum() { /*TODO:propertyler buraya yazılacak ST_OgrenciDurumId = 1, ST_OgrenciDurumName = "test"*/ } });

            var handler = new GetST_OgrenciDurumsQueryHandler(_sT_OgrenciDurumRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_OgrenciDurum>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_OgrenciDurum_CreateCommand_Success()
        {
            ST_OgrenciDurum rt = null;
            //Arrange
            var command = new CreateST_OgrenciDurumCommand();
            //propertyler buraya yazılacak
            //command.ST_OgrenciDurumName = "deneme";

            _sT_OgrenciDurumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgrenciDurum, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_OgrenciDurumRepository.Setup(x => x.Add(It.IsAny<ST_OgrenciDurum>())).Returns(new ST_OgrenciDurum());

            var handler = new CreateST_OgrenciDurumCommandHandler(_sT_OgrenciDurumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_OgrenciDurumRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_OgrenciDurum_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_OgrenciDurumCommand();
            //propertyler buraya yazılacak 
            //command.ST_OgrenciDurumName = "test";

            _sT_OgrenciDurumRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_OgrenciDurum> { new ST_OgrenciDurum() { /*TODO:propertyler buraya yazılacak ST_OgrenciDurumId = 1, ST_OgrenciDurumName = "test"*/ } }.AsQueryable());

            _sT_OgrenciDurumRepository.Setup(x => x.Add(It.IsAny<ST_OgrenciDurum>())).Returns(new ST_OgrenciDurum());

            var handler = new CreateST_OgrenciDurumCommandHandler(_sT_OgrenciDurumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_OgrenciDurum_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_OgrenciDurumCommand();
            //command.ST_OgrenciDurumName = "test";

            _sT_OgrenciDurumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgrenciDurum, bool>>>()))
                        .ReturnsAsync(new ST_OgrenciDurum() { /*TODO:propertyler buraya yazılacak ST_OgrenciDurumId = 1, ST_OgrenciDurumName = "deneme"*/ });

            _sT_OgrenciDurumRepository.Setup(x => x.Update(It.IsAny<ST_OgrenciDurum>())).Returns(new ST_OgrenciDurum());

            var handler = new UpdateST_OgrenciDurumCommandHandler(_sT_OgrenciDurumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_OgrenciDurumRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_OgrenciDurum_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_OgrenciDurumCommand();

            _sT_OgrenciDurumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgrenciDurum, bool>>>()))
                        .ReturnsAsync(new ST_OgrenciDurum() { /*TODO:propertyler buraya yazılacak ST_OgrenciDurumId = 1, ST_OgrenciDurumName = "deneme"*/});

            _sT_OgrenciDurumRepository.Setup(x => x.Delete(It.IsAny<ST_OgrenciDurum>()));

            var handler = new DeleteST_OgrenciDurumCommandHandler(_sT_OgrenciDurumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_OgrenciDurumRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

