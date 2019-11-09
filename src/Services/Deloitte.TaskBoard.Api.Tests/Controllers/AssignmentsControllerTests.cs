using AutoMapper;
using Deloitte.TaskBoard.Api.Controllers;
using Deloitte.TaskBoard.Api.Models.Dtos;
using Deloitte.TaskBoard.Domain.Models;
using Deloitte.TaskBoard.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.TaskBoard.Api.Tests.Controllers
{
    [TestFixture]
    [Category("Unit")]
    public class AssignmentsControllerTests
    {
        private Mock<IAssignmentRepository> _repository;
        private Mock<IMapper> _mapper;
        private AssignmentsController _controller;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IAssignmentRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new AssignmentsController(_repository.Object, _mapper.Object);
        }

        [Test]
        public async Task GetAll_RepositoryIsCalled()
        {
            //Arrange & Act
            await _controller.GetAll();

            //Assert
            _repository.Verify(x => x.GetAll(), Times.Once);
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetAll_RecordsAreFound_MapperIsCalled()
        {
            //Arrange
            _repository.Setup(x => x.GetAll())
                .ReturnsAsync(new List<Assignment>());

            //Act
            await _controller.GetAll();

            //Assert
            _mapper.Verify(x => x.Map<List<AssignmentDto>>(It.IsAny<List<Assignment>>()));
            _mapper.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetAll_RecordsAreFound_OkWithAssignmentsListIsReturned()
        {
            //Arrange
            _repository.Setup(x => x.GetAll())
                .ReturnsAsync(new List<Assignment> { new Assignment() });
            _mapper.Setup(x => x.Map<List<AssignmentDto>>(It.IsAny<List<Assignment>>()))
                .Returns(new List<AssignmentDto> { new AssignmentDto() });

            //Act
            var result = await _controller.GetAll();

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as OkObjectResult;
            var assignmentsList = response.Value as List<AssignmentDto>;

            Assert.AreEqual(1, assignmentsList.Count);
        }

        [Test]
        public async Task GetAll_RecordsAreNotFound_OkWithEmptyListIsReturned()
        {
            //Arrange
            _repository.Setup(x => x.GetAll())
                .ReturnsAsync(new List<Assignment>());
            _mapper.Setup(x => x.Map<List<AssignmentDto>>(It.IsAny<List<Assignment>>()))
                .Returns(new List<AssignmentDto>());

            //Act
            var result = await _controller.GetAll();

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as OkObjectResult;
            var assignmentsList = response.Value as List<AssignmentDto>;

            Assert.AreEqual(0, assignmentsList.Count);
        }

        [Test]
        public async Task FindById_RepositoryIsCalled()
        {
            //Arrange & Act
            await _controller.FindById(Guid.NewGuid());

            //Assert
            _repository.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public async Task FindById_RecordsAreFound_MapperIsCalled()
        {
            //Arrange
            _repository.Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(new Assignment());

            //Act
            await _controller.FindById(Guid.NewGuid());

            //Assert
            _mapper.Verify(x => x.Map<AssignmentDto>(It.IsAny<Assignment>()));
            _mapper.VerifyNoOtherCalls();
        }

        [Test]
        public async Task FindById_RecordIsFound_OkWithAssignmentIsReturned()
        {
            //Arrange
            _repository.Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(new Assignment());
            _mapper.Setup(x => x.Map<AssignmentDto>(It.IsAny<Assignment>()))
                .Returns(new AssignmentDto());

            //Act
            var result = await _controller.FindById(Guid.NewGuid());

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as OkObjectResult;
            var assignment = response.Value as AssignmentDto;

            Assert.IsNotNull(assignment);
        }

        [Test]
        public async Task FindById_NoRecordIsFound_NotFoundIsReturned()
        {
            //Arrange
            _repository.Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(default(Assignment));

            //Act
            var result = await _controller.FindById(Guid.NewGuid());

            //Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Create_MapperAndRepositoryAreCalled()
        {
            //Arrange 
            _mapper.Setup(x => x.Map<Assignment>(It.IsAny<CreateAssignmentDto>()))
                .Returns(new Assignment());
            _repository.Setup(x => x.Create(It.IsAny<Assignment>()))
                .ReturnsAsync(new Assignment());
            _mapper.Setup(x => x.Map<AssignmentDto>(It.IsAny<Assignment>()))
                .Returns(new AssignmentDto());

            //Act
            await _controller.Create(new CreateAssignmentDto());

            //Assert
            _mapper.Verify(x => x.Map<Assignment>(It.IsAny<CreateAssignmentDto>()), Times.Once);
            _mapper.Verify(x => x.Map<AssignmentDto>(It.IsAny<Assignment>()), Times.Once);
            _mapper.VerifyNoOtherCalls();
            _repository.Verify(x => x.Create(It.IsAny<Assignment>()), Times.Once);
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Create_CreatedAtActionIsReturned()
        {
            //Arrange
            _mapper.Setup(x => x.Map<Assignment>(It.IsAny<CreateAssignmentDto>()))
                .Returns(new Assignment());
            _repository.Setup(x => x.Create(It.IsAny<Assignment>()))
                .ReturnsAsync(new Assignment());
            _mapper.Setup(x => x.Map<AssignmentDto>(It.IsAny<Assignment>()))
                .Returns(new AssignmentDto());

            //Act
            var result = await _controller.Create(new CreateAssignmentDto());

            //Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);

            var response = result as CreatedAtActionResult;
            var assignment = response.Value as AssignmentDto;

            Assert.IsNotNull(assignment);
        }

        [Test]
        public async Task Update_RepositoryFindByIdIsCalled()
        {
            //Arrange & Act
            await _controller.Update(Guid.NewGuid(), new UpdateAssignmentDto());

            //Assert
            _repository.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Update_NoRecordIsFound_NotFoundIsReturned()
        {
            //Arrange
            _repository.Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(default(Assignment));

            //Act
            var result = await _controller.Update(Guid.NewGuid(), new UpdateAssignmentDto());

            //Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Update_RecordIsFound_MapperAndRepositoryAreCalled()
        {
            //Arrange
            _repository.Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(new Assignment());
            _repository.Setup(x => x.Update(It.IsAny<Assignment>()))
                .ReturnsAsync(new Assignment());
            _mapper.Setup(x => x.Map<AssignmentDto>(It.IsAny<Assignment>()))
                .Returns(new AssignmentDto());

            //Act
            var result = await _controller.Update(Guid.NewGuid(), new UpdateAssignmentDto());

            //Assert
            _repository.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
            _repository.Verify(x => x.Update(It.IsAny<Assignment>()), Times.Once);
            _repository.VerifyNoOtherCalls();
            _mapper.Verify(x => x.Map(It.IsAny<UpdateAssignmentDto>(), It.IsAny<Assignment>()), Times.Once);
            _mapper.Verify(x => x.Map<AssignmentDto>(It.IsAny<Assignment>()), Times.Once);
            _mapper.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Update_RecordIsUpdated_OkIsReturned()
        {
            //Arrange
            _repository.Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(new Assignment());
            _repository.Setup(x => x.Update(It.IsAny<Assignment>()))
                .ReturnsAsync(new Assignment());
            _mapper.Setup(x => x.Map<AssignmentDto>(It.IsAny<Assignment>()))
                .Returns(new AssignmentDto());

            //Act
            var result = await _controller.Update(Guid.NewGuid(), new UpdateAssignmentDto());

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as OkObjectResult;
            var assignment = response.Value as AssignmentDto;

            Assert.IsNotNull(assignment);
        }

        [Test]
        public async Task Delete_RepositoryIsCalled()
        {
            //Arrange & Act
            await _controller.Delete(Guid.NewGuid());

            //Assert
            _repository.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Delete_NoRecordIsFound_NotFoundIsReturned()
        {
            //Arrange
            _repository.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            //Act
            var result = await _controller.Delete(Guid.NewGuid());

            //Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Delete_RecordIsFound_OkIsReturned()
        {
            //Arrange
            _repository.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var result = await _controller.Delete(Guid.NewGuid());

            //Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
