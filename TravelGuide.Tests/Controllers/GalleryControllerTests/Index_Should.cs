using System;
using System.Collections.Generic;
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
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultViewWithCorrectModel_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            galleryServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var images = new List<GalleryImage>();
            galleryServiceMock.Setup(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(images);

            var mappedImages = new List<GalleryItemViewModel>() { new GalleryItemViewModel() };
            mappingServiceMock.Setup(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>())).Returns(mappedImages);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var model = new GalleryListViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryListViewModel>(mappedImages)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.Index(null, null))
                .ShouldRenderDefaultView()
                .WithModel<GalleryListViewModel>(x => x == model);
        }

        [Test]
        public void CallGetPagesCount_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            galleryServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var images = new List<GalleryImage>();
            galleryServiceMock.Setup(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(images);

            var mappedImages = new List<GalleryItemViewModel>() { new GalleryItemViewModel() };
            mappingServiceMock.Setup(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>())).Returns(mappedImages);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var model = new GalleryListViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryListViewModel>(mappedImages)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            galleryServiceMock.Verify(x => x.GetPagesCount(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallGetPageMethod_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            galleryServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var images = new List<GalleryImage>();
            galleryServiceMock.Setup(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(images);

            var mappedImages = new List<GalleryItemViewModel>() { new GalleryItemViewModel() };
            mappingServiceMock.Setup(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>())).Returns(mappedImages);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var model = new GalleryListViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryListViewModel>(mappedImages)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            utilsMock.Verify(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallGetFilteredImagesByPageMethod_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            galleryServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var images = new List<GalleryImage>();
            galleryServiceMock.Setup(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(images);

            var mappedImages = new List<GalleryItemViewModel>() { new GalleryItemViewModel() };
            mappingServiceMock.Setup(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>())).Returns(mappedImages);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var model = new GalleryListViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryListViewModel>(mappedImages)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            galleryServiceMock.Verify(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
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

            galleryServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var images = new List<GalleryImage>();
            galleryServiceMock.Setup(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(images);

            var mappedImages = new List<GalleryItemViewModel>() { new GalleryItemViewModel() };
            mappingServiceMock.Setup(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>())).Returns(mappedImages);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var model = new GalleryListViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryListViewModel>(mappedImages)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            mappingServiceMock.Verify(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>()), Times.Once);
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

            galleryServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var images = new List<GalleryImage>();
            galleryServiceMock.Setup(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(images);

            var mappedImages = new List<GalleryItemViewModel>() { new GalleryItemViewModel() };
            mappingServiceMock.Setup(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>())).Returns(mappedImages);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var model = new GalleryListViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryListViewModel>(mappedImages)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            userServiceMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallIsImageLiked_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            galleryServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var images = new List<GalleryImage>();
            galleryServiceMock.Setup(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(images);

            var mappedImages = new List<GalleryItemViewModel>() { new GalleryItemViewModel() };
            mappingServiceMock.Setup(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>())).Returns(mappedImages);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var model = new GalleryListViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryListViewModel>(mappedImages)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            galleryServiceMock.Verify(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallMaptoGalleryList_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            galleryServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var images = new List<GalleryImage>();
            galleryServiceMock.Setup(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(images);

            var mappedImages = new List<GalleryItemViewModel>() { new GalleryItemViewModel() };
            mappingServiceMock.Setup(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>())).Returns(mappedImages);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var model = new GalleryListViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryListViewModel>(mappedImages)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            mappingServiceMock.Verify(x => x.Map<GalleryListViewModel>(mappedImages), Times.Once);
        }

        [Test]
        public void CallAssignViewParams_WhenParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            galleryServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var images = new List<GalleryImage>();
            galleryServiceMock.Setup(x => x.GetFilteredImagesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(images);

            var mappedImages = new List<GalleryItemViewModel>() { new GalleryItemViewModel() };
            mappingServiceMock.Setup(x => x.Map<IEnumerable<GalleryItemViewModel>>(It.IsAny<IEnumerable<GalleryImage>>())).Returns(mappedImages);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            galleryServiceMock.Setup(x => x.IsImageLiked(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var model = new GalleryListViewModel();
            mappingServiceMock.Setup(x => x.Map<GalleryListViewModel>(mappedImages)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            utilsMock.Verify(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }
    }
}