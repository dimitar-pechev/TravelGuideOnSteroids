using System;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Blog.Controllers;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Gallery;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Gallery.Contacts;

namespace TravelGuide.Tests.Controllers.GalleryControllerTests
{
    [TestFixture]
    public class DeleteComment_Should
    {
        public void ReturnBadRequest_WhenNoImageIdIsPassed()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteComment(Guid.NewGuid(), null))
                .ShouldGiveHttpStatus(400);
        }

        public void ReturnHttpNotFound_WhenNoImageIsFound()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteComment(Guid.NewGuid(), Guid.NewGuid()))
                .ShouldGiveHttpStatus(404);
        }

        public void ReturnBadRequest_WhenNoCommentIdIsPassed()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteComment(null, Guid.NewGuid()))
                .ShouldGiveHttpStatus(400);
        }

        public void ReturnCommentBoxPartialWithCorrectModel_WhenParamsAreCorrect()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            galleryServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(It.IsAny<GalleryImage>())).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteComment(Guid.NewGuid(), Guid.NewGuid()))
                .ShouldRenderPartialView("_CommentBoxPartial")
                .WithModel<GalleryItemViewModel>(x => x == model);
        }

        public void CallGetImageById_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            galleryServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(It.IsAny<GalleryImage>())).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.DeleteComment(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            galleryServiceMock.Verify(x => x.GetGalleryImageById(It.IsAny<Guid>()), Times.Exactly(2));
        }

        public void CallDeleteComment_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            galleryServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(It.IsAny<GalleryImage>())).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.DeleteComment(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            galleryServiceMock.Verify(x => x.DeleteComment(It.IsAny<string>()), Times.Once);
        }

        public void CallMapToGalleryItem_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            galleryServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(It.IsAny<GalleryImage>())).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.DeleteComment(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            mappingServiceMock.Verify(x => x.Map<GalleryItemViewModel>(It.IsAny<GalleryImage>()), Times.Once);
        }

        public void CallGetUserById_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            galleryServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(It.IsAny<GalleryImage>())).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.DeleteComment(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            userServiceMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }
    }
}