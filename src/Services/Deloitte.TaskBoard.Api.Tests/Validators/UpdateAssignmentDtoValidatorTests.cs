using Deloitte.TaskBoard.Api.Models.Dtos;
using Deloitte.TaskBoard.Api.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Deloitte.TaskBoard.Api.Tests.Validators
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateAssignmentDtoValidatorTests
    {
        private UpdateAssignmentDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UpdateAssignmentDtoValidator();
        }

        [Test]
        public void Validate_TitleIsEmpty_ShouldHaveError()
        {
            var dto = new UpdateAssignmentDto
            {
                Title = string.Empty,
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Title, dto);
        }

        [Test]
        public void Validate_DescriptionIsEmpty_ShouldHaveError()
        {
            var dto = new UpdateAssignmentDto
            {
                Description = string.Empty,
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Description, dto);
        }

        [Test]
        public void Validate_RequesterIsEmpty_ShouldHaveError()
        {
            var dto = new UpdateAssignmentDto
            {
                Requester = string.Empty,
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Requester, dto);
        }

        [Test]
        public void Validate_StatusIsEmpty_ShouldHaveError()
        {
            var dto = new UpdateAssignmentDto
            {
                Status = string.Empty,
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Status, dto);
        }

        [Test]
        public void Validate_PriorityIsEmpty_ShouldHaveError()
        {
            var dto = new UpdateAssignmentDto
            {
                Priority = string.Empty,
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Priority, dto);
        }

        [Test]
        public void Validate_StatusIsInvalid_ShouldHaveError()
        {
            var dto = new UpdateAssignmentDto
            {
                Status = "Finished",
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Status, dto);
        }

        [Test]
        public void Validate_PriorityIsInvalid_ShouldHaveError()
        {
            var dto = new UpdateAssignmentDto
            {
                Priority = "Critical",
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Priority, dto);
        }

        [Test]
        public void Validate_OrderIsZero_ShouldHaveError()
        {
            var dto = new UpdateAssignmentDto
            {
                Order = 0
            };

            _validator.ShouldHaveValidationErrorFor(x => x.Order, dto);
        }

        [Test]
        public void Validate_DtoIsValid_ShouldNotHaveError()
        {
            var dto = new UpdateAssignmentDto
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
