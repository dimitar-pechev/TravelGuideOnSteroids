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
    public class PopulateModal_Should
    {
        [Test]
        public void ReturnBadRequest_WhenImageIdIsMissing()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.PopulateModal(null))
                .ShouldGiveHttpStatus(400);
        }

        [Test]
        public void ReturnHttpNotFound_WhenNoSuchImageIsPresent()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.PopulateModal(Guid.NewGuid()))
                .ShouldGiveHttpStatus(404);
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

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);
            var ids = new Tuple<Guid, Guid>(Guid.NewGuid(), Guid.NewGuid());
            galleryServiceMock.Setup(x => x.GetSurroundingImageIds(It.IsAny<GalleryImage>())).Returns(ids);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            // Act & Assert
            controller.WithCallTo(x => x.PopulateModal(Guid.NewGuid()))
                .ShouldRenderPartialView("_ImageDetailsPartial")
                .WithModel<GalleryItemViewModel>();
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

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);
            var ids = new Tuple<Guid, Guid>(Guid.NewGuid(), Guid.NewGuid());
            galleryServiceMock.Setup(x => x.GetSurroundingImageIds(It.IsAny<GalleryImage>())).Returns(ids);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            // Act
            controller.PopulateModal(Guid.NewGuid());

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

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);
            var ids = new Tuple<Guid, Guid>(Guid.NewGuid(), Guid.NewGuid());
            galleryServiceMock.Setup(x => x.GetSurroundingImageIds(It.IsAny<GalleryImage>())).Returns(ids);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            // Act
            controller.PopulateModal(Guid.NewGuid());

            // Assert
            mappingServiceMock.Verify(x => x.Map<GalleryItemViewModel>(image), Times.Once);
        }

        [Test]
        public void CallGetSurroundingImageIds_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);
            var ids = new Tuple<Guid, Guid>(Guid.NewGuid(), Guid.NewGuid());
            galleryServiceMock.Setup(x => x.GetSurroundingImageIds(It.IsAny<GalleryImage>())).Returns(ids);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            // Act
            controller.PopulateModal(Guid.NewGuid());

            // Assert
            galleryServiceMock.Verify(x => x.GetSurroundingImageIds(It.IsAny<GalleryImage>()), Times.Once);
        }

        [Test]
        public void CallUserGetById_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            var model = new GalleryItemViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryItemViewModel>(image)).Returns(model);
            var ids = new Tuple<Guid, Guid>(Guid.NewGuid(), Guid.NewGuid());
            galleryServiceMock.Setup(x => x.GetSurroundingImageIds(It.IsAny<GalleryImage>())).Returns(ids);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            // Act
            controller.PopulateModal(Guid.NewGuid());

            // Assert
            userServiceMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }
    }
}