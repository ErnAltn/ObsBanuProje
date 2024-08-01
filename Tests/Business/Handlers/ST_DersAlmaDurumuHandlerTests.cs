
using Business.Handlers.ST_DersAlmaDurumus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_DersAlmaDurumus.Queries.GetST_DersAlmaDurumuQuery;
using Entities.Concrete;
using static Business.Handlers.ST_DersAlmaDurumus.Queries.GetST_DersAlmaDurumusQuery;
using static Business.Handlers.ST_DersAlmaDurumus.Commands.CreateST_DersAlmaDurumuCommand;
using Business.Handlers.ST_DersAlmaDurumus.Commands;
using Business.Constants;
using static Business.Handlers.ST_DersAlmaDurumus.Commands.UpdateST_DersAlmaDurumuCommand;
using static Business.Handlers.ST_DersAlmaDurumus.Commands.DeleteST_DersAlmaDurumuCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_DersAlmaDurumuHandlerTests
    {
        Mock<IST_DersAlmaDurumuRepository> _sT_DersAlmaDurumuRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_DersAlmaDurumuRepository = new Mock<IST_DersAlmaDurumuRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_DersAlmaDurumu_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_DersAlmaDurumuQuery();

            _sT_DersAlmaDurumuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersAlmaDurumu, bool>>>())).ReturnsAsync(new ST_DersAlmaDurumu()
//propertyler buraya yazılacak
//{																		
//ST_DersAlmaDurumuId = 1,
//ST_DersAlmaDurumuName = "Test"
//}
);

            var handler = new GetST_DersAlmaDurumuQueryHandler(_sT_DersAlmaDurumuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_DersAlmaDurumuId.Should().Be(1);

        }

        [Test]
        public async Task ST_DersAlmaDurumu_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_DersAlmaDurumusQuery();

            _sT_DersAlmaDurumuRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_DersAlmaDurumu, bool>>>()))
                        .ReturnsAsync(new List<ST_DersAlmaDurumu> { new ST_DersAlmaDurumu() { /*TODO:propertyler buraya yazılacak ST_DersAlmaDurumuId = 1, ST_DersAlmaDurumuName = "test"*/ } });

            var handler = new GetST_DersAlmaDurumusQueryHandler(_sT_DersAlmaDurumuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_DersAlmaDurumu>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_DersAlmaDurumu_CreateCommand_Success()
        {
            ST_DersAlmaDurumu rt = null;
            //Arrange
            var command = new CreateST_DersAlmaDurumuCommand();
            //propertyler buraya yazılacak
            //command.ST_DersAlmaDurumuName = "deneme";

            _sT_DersAlmaDurumuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersAlmaDurumu, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_DersAlmaDurumuRepository.Setup(x => x.Add(It.IsAny<ST_DersAlmaDurumu>())).Returns(new ST_DersAlmaDurumu());

            var handler = new CreateST_DersAlmaDurumuCommandHandler(_sT_DersAlmaDurumuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersAlmaDurumuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_DersAlmaDurumu_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_DersAlmaDurumuCommand();
            //propertyler buraya yazılacak 
            //command.ST_DersAlmaDurumuName = "test";

            _sT_DersAlmaDurumuRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_DersAlmaDurumu> { new ST_DersAlmaDurumu() { /*TODO:propertyler buraya yazılacak ST_DersAlmaDurumuId = 1, ST_DersAlmaDurumuName = "test"*/ } }.AsQueryable());

            _sT_DersAlmaDurumuRepository.Setup(x => x.Add(It.IsAny<ST_DersAlmaDurumu>())).Returns(new ST_DersAlmaDurumu());

            var handler = new CreateST_DersAlmaDurumuCommandHandler(_sT_DersAlmaDurumuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_DersAlmaDurumu_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_DersAlmaDurumuCommand();
            //command.ST_DersAlmaDurumuName = "test";

            _sT_DersAlmaDurumuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersAlmaDurumu, bool>>>()))
                        .ReturnsAsync(new ST_DersAlmaDurumu() { /*TODO:propertyler buraya yazılacak ST_DersAlmaDurumuId = 1, ST_DersAlmaDurumuName = "deneme"*/ });

            _sT_DersAlmaDurumuRepository.Setup(x => x.Update(It.IsAny<ST_DersAlmaDurumu>())).Returns(new ST_DersAlmaDurumu());

            var handler = new UpdateST_DersAlmaDurumuCommandHandler(_sT_DersAlmaDurumuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersAlmaDurumuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_DersAlmaDurumu_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_DersAlmaDurumuCommand();

            _sT_DersAlmaDurumuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersAlmaDurumu, bool>>>()))
                        .ReturnsAsync(new ST_DersAlmaDurumu() { /*TODO:propertyler buraya yazılacak ST_DersAlmaDurumuId = 1, ST_DersAlmaDurumuName = "deneme"*/});

            _sT_DersAlmaDurumuRepository.Setup(x => x.Delete(It.IsAny<ST_DersAlmaDurumu>()));

            var handler = new DeleteST_DersAlmaDurumuCommandHandler(_sT_DersAlmaDurumuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersAlmaDurumuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

