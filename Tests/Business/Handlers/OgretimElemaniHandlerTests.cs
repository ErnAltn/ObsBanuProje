
using Business.Handlers.OgretimElemanis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OgretimElemanis.Queries.GetOgretimElemaniQuery;
using Entities.Concrete;
using static Business.Handlers.OgretimElemanis.Queries.GetOgretimElemanisQuery;
using static Business.Handlers.OgretimElemanis.Commands.CreateOgretimElemaniCommand;
using Business.Handlers.OgretimElemanis.Commands;
using Business.Constants;
using static Business.Handlers.OgretimElemanis.Commands.UpdateOgretimElemaniCommand;
using static Business.Handlers.OgretimElemanis.Commands.DeleteOgretimElemaniCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using Nest;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OgretimElemaniHandlerTests
    {
        Mock<IOgretimElemaniRepository> _ogretimElemaniRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _ogretimElemaniRepository = new Mock<IOgretimElemaniRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OgretimElemani_GetQuery_Success()
        {
            //Arrange
            var query = new GetOgretimElemaniQuery();

            _ogretimElemaniRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OgretimElemani, bool>>>())).ReturnsAsync(new OgretimElemani()

                {
                Id = 1,
                Adi = "Test"
                }
);

            var handler = new GetOgretimElemaniQueryHandler(_ogretimElemaniRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OgretimElemaniId.Should().Be(1);

        }

        [Test]
        public async Task OgretimElemani_GetQueries_Success()
        {
            //Arrange
            var query = new GetOgretimElemanisQuery();

            _ogretimElemaniRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OgretimElemani, bool>>>()))
                        .ReturnsAsync(new List<OgretimElemani> { 
                            new OgretimElemani() { Id = 90, Adi = "Abdullah" },
                            new OgretimElemani() { Id = 3, Adi = "Mehmet" },
                            new OgretimElemani() { Id = 4, Adi = "Ayşe" },  });

            var handler = new GetOgretimElemanisQueryHandler(_ogretimElemaniRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OgretimElemani>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task OgretimElemani_CreateCommand_Success()
        {
            OgretimElemani rt = null;
          
            var command = new CreateOgretimElemaniCommand();
            
            command.Adi = "deneme";

            _ogretimElemaniRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OgretimElemani, bool>>>()))
                        .ReturnsAsync(rt);

            _ogretimElemaniRepository.Setup(x => x.Add(It.IsAny<OgretimElemani>())).Returns(new OgretimElemani());

            var handler = new CreateOgretimElemaniCommandHandler(_ogretimElemaniRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ogretimElemaniRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OgretimElemani_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOgretimElemaniCommand();
            //propertyler buraya yazılacak 
            //command.OgretimElemaniName = "test";

            _ogretimElemaniRepository.Setup(x => x.Query())
                                           .Returns(new List<OgretimElemani> { new OgretimElemani() { /*TODO:propertyler buraya yazılacak OgretimElemaniId = 1, OgretimElemaniName = "test"*/ } }.AsQueryable());

            _ogretimElemaniRepository.Setup(x => x.Add(It.IsAny<OgretimElemani>())).Returns(new OgretimElemani());

            var handler = new CreateOgretimElemaniCommandHandler(_ogretimElemaniRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OgretimElemani_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOgretimElemaniCommand();
            //command.OgretimElemaniName = "test";

            _ogretimElemaniRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OgretimElemani, bool>>>()))
                        .ReturnsAsync(new OgretimElemani() { /*TODO:propertyler buraya yazılacak OgretimElemaniId = 1, OgretimElemaniName = "deneme"*/ });

            _ogretimElemaniRepository.Setup(x => x.Update(It.IsAny<OgretimElemani>())).Returns(new OgretimElemani());

            var handler = new UpdateOgretimElemaniCommandHandler(_ogretimElemaniRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ogretimElemaniRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OgretimElemani_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOgretimElemaniCommand();
            command.Id = 2;
            
            _ogretimElemaniRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OgretimElemani, bool>>>()))
                        .ReturnsAsync(new OgretimElemani() {Id = 2});

            _ogretimElemaniRepository.Setup(x => x.Delete(It.IsAny<OgretimElemani>()));

            var handler = new DeleteOgretimElemaniCommandHandler(_ogretimElemaniRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ogretimElemaniRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

