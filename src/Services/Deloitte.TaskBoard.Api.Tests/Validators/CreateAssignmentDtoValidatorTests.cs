using Deloitte.TaskBoard.Api.Models.Dtos;
using Deloitte.TaskBoard.Api.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Deloitte.TaskBoard.Api.Tests.Validators
{
    [TestFixture]
    [Category("Unit")]
    public class CreateAssignmentDtoValidatorTests
    {
        private CreateAssignmentDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateAssignmentDtoValidator();
        }

        [Test]
        public void Validate_TitleIsNull_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Description = "Description",
                Status = "To Do",
                Requester = "Me",
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Title, dto);
        }

        [Test]
        public void Validate_DescriptionIsNull_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Status = "To Do",
                Requester = "Me",
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Description, dto);
        }

        [Test]
        public void Validate_RequesterIsNull_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Status = "To Do",
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Requester, dto);
        }

        [Test]
        public void Validate_StatusIsNull_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = "Me",
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Status, dto);
        }

        [Test]
        public void Validate_PriorityIsNull_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = "Me",
                Status = "To Do",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Priority, dto);
        }

        [Test]
        public void Validate_OrderIsNull_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = "Me",
                Status = "To Do",
                Priority = "Low"
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Order, dto);
        }

        [Test]
        public void Validate_TitleIsEmpty_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = string.Empty,
                Description = "Description",
                Status = "To Do",
                Requester = "Me",
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Title, dto);
        }

        [Test]
        public void Validate_DescriptionIsEmpty_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = string.Empty,
                Status = "To Do",
                Requester = "Me",
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Description, dto);
        }

        [Test]
        public void Validate_RequesterIsEmpty_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = string.Empty,
                Status = "To Do",
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Requester, dto);
        }

        [Test]
        public void Validate_StatusIsEmpty_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = "Me",
                Status = string.Empty,
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Status, dto);
        }

        [Test]
        public void Validate_PriorityIsEmpty_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = "Me",
                Status = "To Do",
                Priority = string.Empty,
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Priority, dto);
        }

        [Test]
        public void Validate_StatusIsInvalid_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = "Me",
                Status = "Finished",
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Status, dto);
        }

        [Test]
        public void Validate_PriorityIsInvalid_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = "Me",
                Status = "To Do",
                Priority = "Critical",
                Order = 1
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Priority, dto);
        }

        [Test]
        public void Validate_OrderIsZero_ShouldHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = "Me",
                Status = "To Do",
                Priority = "Low",
                Order = 0
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Order, dto);
        }

        [Test]
        public void Validate_DtoIsValid_ShouldNotHaveError()
        {
            var dto = new CreateAssignmentDto
            {
                Title = "Title",
                Description = "Description",
                Requester = "Me",
                Status = "To Do",
                Priority = "Low",
                Order = 1
            };

            _validator.ShouldNotHaveValidationErrorFor(x => new { x.Title, x.Description, x.Requester, x.Status, x.Priority, x.Order }, dto);
        }
    }
}
