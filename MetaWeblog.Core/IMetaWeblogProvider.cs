namespace MetaWeblog
{
    using System.Threading.Tasks;

    /// <summary>
    /// The MetaWeblog provider interface.
    /// </summary>
    public interface IMetaWeblogProvider
    {
        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="category">The category.</param>
        /// <returns>A <see cref="Task{Int32}"/>.</returns>
        Task<int> AddCategoryAsync(string key, string username, string password, NewCategory category);

        /// <summary>
        /// Adds a new page.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="page">The page.</param>
        /// <param name="publish">if set to <c>true</c> the page will be marked as published.</param>
        /// <returns>A <see cref="Task{String}"/>.</returns>
        Task<string> AddPageAsync(string blogid, string username, string password, Page page, bool publish);

        /// <summary>
        /// Adds a new post.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="post">The post.</param>
        /// <param name="publish">if set to <c>true</c> the post will be marked as published.</param>
        /// <returns>A <see cref="Task{String}"/>.</returns>
        Task<string> AddPostAsync(string blogid, string username, string password, Post post, bool publish);

        /// <summary>
        /// Deletes the specified page.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="pageid">The page identifier.</param>
        /// <returns>A <see cref="Task{Boolean}"/> indicating whether the page was successfully deleted.</returns>
        Task<bool> DeletePageAsync(string blogid, string username, string password, string pageid);

        /// <summary>
        /// Deletes the specified post.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="postid">The post identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="publish">if set to <c>true</c> the post was marked as published.</param>
        /// <returns>A <see cref="Task{Boolean}"/> indicating whether the post was successfully deleted.</returns>
        Task<bool> DeletePostAsync(string key, string postid, string username, string password, bool publish);

        /// <summary>
        /// Edits the specified page.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="pageid">The page identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="page">The page.</param>
        /// <param name="publish">if set to <c>true</c> the page will be marked as published.</param>
        /// <returns>A <see cref="Task{Boolean}"/> indicating whether the page was successfully edited.</returns>
        Task<bool> EditPageAsync(string blogid, string pageid, string username, string password, Page page, bool publish);

        /// <summary>
        /// Edits the specified post.
        /// </summary>
        /// <param name="postid">The post identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="post">The post.</param>
        /// <param name="publish">if set to <c>true</c> the post will be marked as published.</param>
        /// <returns>A <see cref="Task{Boolean}"/> indicating whether the post was successfully edited.</returns>
        Task<bool> EditPostAsync(string postid, string username, string password, Post post, bool publish);

        /// <summary>
        /// Gets an array of the authors for the specified blog.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task"/> yielding an array of <see cref="Author"/>.</returns>
        Task<Author[]> GetAuthorsAsync(string blogid, string username, string password);

        /// <summary>
        /// Gets an array of the categories for the specified blog.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task"/> yielding an array of <see cref="CategoryInfo"/>.</returns>
        Task<CategoryInfo[]> GetCategoriesAsync(string blogid, string username, string password);

        /// <summary>
        /// Gets the specified post.
        /// </summary>
        /// <param name="postid">The post identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task{Post}"/>.</returns>
        Task<Post> GetPostAsync(string postid, string username, string password);

        /// <summary>
        /// Gets the recent posts for the specified blog.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="numberOfPosts">The number of posts to retrieve.</param>
        /// <returns>A <see cref="Task"/> yielding an array of <see cref="Post"/>.</returns>
        Task<Post[]> GetRecentPostsAsync(string blogid, string username, string password, int numberOfPosts);

        /// <summary>
        /// Gets the specified page.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="pageid">The page identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task{Page}"/>.</returns>
        Task<Page> GetPageAsync(string blogid, string pageid, string username, string password);

        /// <summary>
        /// Gets the pages.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="numPages">The number pages.</param>
        /// <returns>A <see cref="Task"/> yielding an array of <see cref="Page"/>.</returns>
        Task<Page[]> GetPagesAsync(string blogid, string username, string password, int numPages);

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task{UserInfo}"/>.</returns>
        Task<UserInfo> GetUserInfoAsync(string key, string username, string password);

        /// <summary>
        /// Gets the blogs for the specified user.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task"/> yielding an array of <see cref="BlogInfo"/>.</returns>
        Task<BlogInfo[]> GetUsersBlogsAsync(string key, string username, string password);

        /// <summary>
        /// Creates a new media object.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="mediaObject">The media object.</param>
        /// <returns>A <see cref="Task{MediaObjectInfo}"/>.</returns>
        Task<MediaObjectInfo> NewMediaObjectAsync(string blogid, string username, string password, MediaObject mediaObject);
    }
}
