
using Business.Handlers.ST_AkademikDonems.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_AkademikDonems.Queries.GetST_AkademikDonemQuery;
using Entities.Concrete;
using static Business.Handlers.ST_AkademikDonems.Queries.GetST_AkademikDonemsQuery;
using static Business.Handlers.ST_AkademikDonems.Commands.CreateST_AkademikDonemCommand;
using Business.Handlers.ST_AkademikDonems.Commands;
using Business.Constants;
using static Business.Handlers.ST_AkademikDonems.Commands.UpdateST_AkademikDonemCommand;
using static Business.Handlers.ST_AkademikDonems.Commands.DeleteST_AkademikDonemCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_AkademikDonemHandlerTests
    {
        Mock<IST_AkademikDonemRepository> _sT_AkademikDonemRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_AkademikDonemRepository = new Mock<IST_AkademikDonemRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_AkademikDonem_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_AkademikDonemQuery();

            _sT_AkademikDonemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_AkademikDonem, bool>>>())).ReturnsAsync(new ST_AkademikDonem()
//propertyler buraya yazılacak
//{																		
//ST_AkademikDonemId = 1,
//ST_AkademikDonemName = "Test"
//}
);

            var handler = new GetST_AkademikDonemQueryHandler(_sT_AkademikDonemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_AkademikDonemId.Should().Be(1);

        }

        [Test]
        public async Task ST_AkademikDonem_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_AkademikDonemsQuery();

            _sT_AkademikDonemRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_AkademikDonem, bool>>>()))
                        .ReturnsAsync(new List<ST_AkademikDonem> { new ST_AkademikDonem() { /*TODO:propertyler buraya yazılacak ST_AkademikDonemId = 1, ST_AkademikDonemName = "test"*/ } });

            var handler = new GetST_AkademikDonemsQueryHandler(_sT_AkademikDonemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_AkademikDonem>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_AkademikDonem_CreateCommand_Success()
        {
            ST_AkademikDonem rt = null;
            //Arrange
            var command = new CreateST_AkademikDonemCommand();
            //propertyler buraya yazılacak
            //command.ST_AkademikDonemName = "deneme";

            _sT_AkademikDonemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_AkademikDonem, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_AkademikDonemRepository.Setup(x => x.Add(It.IsAny<ST_AkademikDonem>())).Returns(new ST_AkademikDonem());

            var handler = new CreateST_AkademikDonemCommandHandler(_sT_AkademikDonemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_AkademikDonemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_AkademikDonem_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_AkademikDonemCommand();
            //propertyler buraya yazılacak 
            //command.ST_AkademikDonemName = "test";

            _sT_AkademikDonemRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_AkademikDonem> { new ST_AkademikDonem() { /*TODO:propertyler buraya yazılacak ST_AkademikDonemId = 1, ST_AkademikDonemName = "test"*/ } }.AsQueryable());

            _sT_AkademikDonemRepository.Setup(x => x.Add(It.IsAny<ST_AkademikDonem>())).Returns(new ST_AkademikDonem());

            var handler = new CreateST_AkademikDonemCommandHandler(_sT_AkademikDonemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_AkademikDonem_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_AkademikDonemCommand();
            //command.ST_AkademikDonemName = "test";

            _sT_AkademikDonemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_AkademikDonem, bool>>>()))
                        .ReturnsAsync(new ST_AkademikDonem() { /*TODO:propertyler buraya yazılacak ST_AkademikDonemId = 1, ST_AkademikDonemName = "deneme"*/ });

            _sT_AkademikDonemRepository.Setup(x => x.Update(It.IsAny<ST_AkademikDonem>())).Returns(new ST_AkademikDonem());

            var handler = new UpdateST_AkademikDonemCommandHandler(_sT_AkademikDonemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_AkademikDonemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_AkademikDonem_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_AkademikDonemCommand();

            _sT_AkademikDonemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_AkademikDonem, bool>>>()))
                        .ReturnsAsync(new ST_AkademikDonem() { /*TODO:propertyler buraya yazılacak ST_AkademikDonemId = 1, ST_AkademikDonemName = "deneme"*/});

            _sT_AkademikDonemRepository.Setup(x => x.Delete(It.IsAny<ST_AkademikDonem>()));

            var handler = new DeleteST_AkademikDonemCommandHandler(_sT_AkademikDonemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_AkademikDonemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

