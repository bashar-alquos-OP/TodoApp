using Moq;
using TodoApp.Application.DTO.TaskDTO;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Tests
{
    public class TodoServiceTests
    {


        [Fact]
        public async Task GetTaskByIdAsync_Returns_Response()
        {
            
            var repoMock = new Mock<ITodoRepository>();
            repoMock.Setup(repo => repo.GetByIdAsync(id: It.IsAny<int>()))
                              .ReturnsAsync(new TodoTask { Id = 1, Title = "Task1", Content="Content1",UserId= 3 });

            var todoService = new TodoService(repoMock.Object);

            var task = await todoService.GetTaskByIdAsync(userId:3,id:1);

            
            Assert.NotNull(task);
            Assert.Equal("Task1", task.Title);
            Assert.Equal("Content1", task.Content);
            Assert.False(task.IsCompleted);
        }


        [Fact]
        public async Task GetTaskByIdAsync_WhenUserIdNotMatch_ShouldReturnNull()
        {

            var repoMock = new Mock<ITodoRepository>();
            repoMock.Setup(repo => repo.GetByIdAsync(id: It.IsAny<int>()))
                              .ReturnsAsync(new TodoTask { Id = 1, Title = "Task1", Content = "Content1", UserId = 3 });

            var todoService = new TodoService(repoMock.Object);

            var task = await todoService.GetTaskByIdAsync(userId: 1, id: 1);

            
            Assert.Null(task);
        }


        [Fact]
        public async Task CreateTaskAsync_Returns_Response()
        {

            var repoMock = new Mock<ITodoRepository>();
            repoMock.Setup(repo => repo.AddAsync(It.IsAny<TodoTask>()))
                    .Callback<TodoTask>(t => t.Id++);

            var todoService = new TodoService(repoMock.Object);

            var request = new CreateTodoRequest { Title = "Task1", Content = "Content1" };

            var task = await todoService.CreateTaskAsync(userId: 1, request: request);

            
            Assert.NotNull(task);
            Assert.Equal(1, task.Id);
            Assert.Equal("Task1", task.Title);
            Assert.False(task.IsCompleted);
        }

        [Fact]
        public async Task UpdateTaskAsync_WhenUpdateTitle_Returns_True()
        {

            var repoMock = new Mock<ITodoRepository>();
            repoMock.Setup(repo => repo.GetByIdAsync(id: It.IsAny<int>()))
                              .ReturnsAsync(new TodoTask { Id = 1, Title = "Task1", Content = "Content1", UserId = 3 });

            repoMock.Setup(repo => repo.UpdateAsync(It.IsAny<TodoTask>()));

            var todoService = new TodoService(repoMock.Object);

            var request = new UpdateTodoRequest { Title = "Task3" };

            var isUpdated = await todoService.UpdateTaskAsync(id:1 ,userId: 3, request: request);

            Assert.True(isUpdated);
        }

        [Fact]
        public async Task UpdateTaskAsync_WhenTaskNotFound_Returns_False()
        {

            var repoMock = new Mock<ITodoRepository>();
            repoMock.Setup(repo => repo.GetByIdAsync(id: It.IsAny<int>()))
                    .ReturnsAsync((TodoTask?)null);

            repoMock.Setup(repo => repo.UpdateAsync(It.IsAny<TodoTask>()));

            var todoService = new TodoService(repoMock.Object);

            var request = new UpdateTodoRequest { Title = "Task3" };

            var isUpdated = await todoService.UpdateTaskAsync(id: 1, userId: 3, request: request);

            Assert.False(isUpdated);
        }

        [Fact]
        public async Task UpdateTaskAsync_WhenUserIdNotMatch_Returns_False()
        {

            var repoMock = new Mock<ITodoRepository>();
            repoMock.Setup(repo => repo.GetByIdAsync(id: It.IsAny<int>()))
                              .ReturnsAsync(new TodoTask { Id = 1, Title = "Task1", Content = "Content1", UserId = 3 });


            repoMock.Setup(repo => repo.UpdateAsync(It.IsAny<TodoTask>()));

            var todoService = new TodoService(repoMock.Object);

            var request = new UpdateTodoRequest { Title = "Task3" };

            var isUpdated = await todoService.UpdateTaskAsync(id: 1, userId: 5, request: request);

            Assert.False(isUpdated);
        }


        public async Task DeleteTaskAsync_WhenUpdateTitle_Returns_True()
        {

            var repoMock = new Mock<ITodoRepository>();
            repoMock.Setup(repo => repo.GetByIdAsync(id: It.IsAny<int>()))
                              .ReturnsAsync(new TodoTask { Id = 1, Title = "Task1", Content = "Content1", UserId = 3 });

            repoMock.Setup(repo => repo.DeleteAsync(It.IsAny<TodoTask>()));

            var todoService = new TodoService(repoMock.Object);

            

            var isDeleted = await todoService.DeleteTaskAsync(id: 1, userId: 3);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteTaskAsync_WhenTaskNotFound_Returns_False()
        {

            var repoMock = new Mock<ITodoRepository>();
            repoMock.Setup(repo => repo.GetByIdAsync(id: It.IsAny<int>()))
                    .ReturnsAsync((TodoTask?)null);

            repoMock.Setup(repo => repo.DeleteAsync(It.IsAny<TodoTask>()));

            var todoService = new TodoService(repoMock.Object);


            var isDeleted = await todoService.DeleteTaskAsync(id: 1, userId: 3);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteTaskAsync_WhenUserIdNotMatch_Returns_False()
        {

            var repoMock = new Mock<ITodoRepository>();
            repoMock.Setup(repo => repo.GetByIdAsync(id: It.IsAny<int>()))
                              .ReturnsAsync(new TodoTask { Id = 1, Title = "Task1", Content = "Content1", UserId = 3 });


            repoMock.Setup(repo => repo.DeleteAsync(It.IsAny<TodoTask>()));

            var todoService = new TodoService(repoMock.Object);


            var isDeleted = await todoService.DeleteTaskAsync(id: 1, userId: 5);

            Assert.False(isDeleted);
        }



    }
}
