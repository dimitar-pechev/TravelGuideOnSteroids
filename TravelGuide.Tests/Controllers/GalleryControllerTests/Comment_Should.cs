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
    public class Comment_Should
    {
        [Test]
        public void ThrowInvalidOperationException_WhenModelStateIsInvalid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            controller.ViewData.ModelState.AddModelError("asdasda", "asdasdasd");

            var model = new GalleryItemViewModel();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => controller.Comment(Guid.NewGuid(), model));
        }

        [Test]
        public void RenderPartialViewWithCorrectModel_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            galleryServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act & Assert
            controller.WithCallTo(x => x.Comment(Guid.NewGuid(), model))
                .ShouldRenderPartialView("_CommentBoxPartial")
                .WithModel<GalleryItemViewModel>(x => x == model);
        }

        [Test]
        public void CallAddCommentMethod_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            galleryServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.Comment(Guid.NewGuid(), model);

            // Assert
            galleryServiceMock.Verify(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallGetGalleryImageById_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            galleryServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.Comment(Guid.NewGuid(), model);

            // Assert
            galleryServiceMock.Verify(x => x.GetGalleryImageById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallMapToGalleryItem_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            galleryServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.Comment(Guid.NewGuid(), model);

            // Assert
            mappingServiceMock.Verify(x => x.Map<GalleryItemViewModel>(image), Times.Once);
        }

        [Test]
        public void CallGetById_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            galleryServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.Comment(Guid.NewGuid(), model);

            // Assert
            userServiceMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }
    }
}