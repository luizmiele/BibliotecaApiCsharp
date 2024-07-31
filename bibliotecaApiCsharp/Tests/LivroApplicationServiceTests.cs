/*
 namespace bibliotecaApiCsharp.Tests;

public class LivroApplicationServiceTests
{
    [Fact]
    public async Task AddBookAsync_ShouldAddBook()
    {
        // Arrange
        var bookRepositoryMock = new Mock<IBookRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var loanRepositoryMock = new Mock<ILoanRepository>();
        var libraryService = new LibraryService(bookRepositoryMock.Object, userRepositoryMock.Object, loanRepositoryMock.Object);

        var book = new Book { Title = "Test Book", Author = "Author", IsAvailable = true };

        // Act
        await libraryService.AddBookAsync(book);

        // Assert
        bookRepositoryMock.Verify(repo => repo.AddBookAsync(It.IsAny<Book>()), Times.Once);
    }
}
*/