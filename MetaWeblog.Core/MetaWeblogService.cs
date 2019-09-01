// <copyright file="MetaWeblogService.cs" company="improvGroup, LLC">
//     Copyright © 2019 improvGroup, LLC. All Rights Reserved.
// </copyright>
namespace MetaWeblog
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The MetaWeblog service.
    /// Implements the <see cref="MetaWeblog.XmlRpcService" />
    /// Implements the <see cref="MetaWeblog.IMetaWeblogProvider" />
    /// </summary>
    /// <seealso cref="MetaWeblog.XmlRpcService" />
    /// <seealso cref="MetaWeblog.IMetaWeblogProvider" />
    public class MetaWeblogService : XmlRpcService, IMetaWeblogProvider
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<MetaWeblogService> logger;

        /// <summary>
        /// The provider
        /// </summary>
        private readonly IMetaWeblogProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaWeblogService" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="logger">The logger.</param>
        public MetaWeblogService(IMetaWeblogProvider provider, ILogger<MetaWeblogService> logger) : base(logger)
        {
            this.logger = logger;
            this.provider = provider;
        }

        /// <summary>
        /// get users blogs as an asynchronous operation.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task" /> yielding an array of <see cref="T:MetaWeblog.BlogInfo" />.</returns>
        [XmlRpcMethod("blogger.getUsersBlogs")]
        public async Task<BlogInfo[]> GetUsersBlogsAsync(string key, string username, string password)
        {
            this.logger.LogInformation($"MetaWeblog:GetUserBlogs is called");
            return await this.provider.GetUsersBlogsAsync(key, username, password);
        }

        /// <summary>
        /// get user information as an asynchronous operation.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task{UserInfo}" />.</returns>
        [XmlRpcMethod("blogger.getUserInfo")]
        public async Task<UserInfo> GetUserInfoAsync(string key, string username, string password)
        {
            this.logger.LogInformation($"MetaWeblog:GetUserInfo is called");
            return await this.provider.GetUserInfoAsync(key, username, password);
        }

        /// <summary>
        /// add category as an asynchronous operation.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="category">The category.</param>
        /// <returns>A <see cref="Task{Int32}" />.</returns>
        [XmlRpcMethod("wp.newCategory")]
        public async Task<int> AddCategoryAsync(string key, string username, string password, NewCategory category)
        {
            this.logger.LogInformation($"MetaWeblog:AddCategory is called");
            return await this.provider.AddCategoryAsync(key, username, password, category);
        }

        /// <summary>
        /// get post as an asynchronous operation.
        /// </summary>
        /// <param name="postid">The post identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task{Post}" />.</returns>
        [XmlRpcMethod("metaWeblog.getPost")]
        public async Task<Post> GetPostAsync(string postid, string username, string password)
        {
            this.logger.LogInformation($"MetaWeblog:GetPost is called");
            return await this.provider.GetPostAsync(postid, username, password);
        }

        /// <summary>
        /// get recent posts as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="numberOfPosts">The number of posts to retrieve.</param>
        /// <returns>A <see cref="Task" /> yielding an array of <see cref="Post" />.</returns>
        [XmlRpcMethod("metaWeblog.getRecentPosts")]
        public async Task<Post[]> GetRecentPostsAsync(string blogid, string username, string password, int numberOfPosts)
        {
            this.logger.LogInformation($"MetaWeblog:GetRecentPosts is called");
            return await this.provider.GetRecentPostsAsync(blogid, username, password, numberOfPosts);
        }

        /// <summary>
        /// add post as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="post">The post.</param>
        /// <param name="publish">if set to <c>true</c> the post will be marked as published.</param>
        /// <returns>A <see cref="Task{String}" />.</returns>
        [XmlRpcMethod("metaWeblog.newPost")]
        public async Task<string> AddPostAsync(string blogid, string username, string password, Post post, bool publish)
        {
            this.logger.LogInformation($"MetaWeblog:AddPost is called");
            return await this.provider.AddPostAsync(blogid, username, password, post, publish);
        }

        /// <summary>
        /// edit post as an asynchronous operation.
        /// </summary>
        /// <param name="postid">The post identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="post">The post.</param>
        /// <param name="publish">if set to <c>true</c> the post will be marked as published.</param>
        /// <returns>A <see cref="Task{Boolean}" /> indicating whether the post was successfully edited.</returns>
        [XmlRpcMethod("metaWeblog.editPost")]
        public async Task<bool> EditPostAsync(string postid, string username, string password, Post post, bool publish)
        {
            this.logger.LogInformation($"MetaWeblog:EditPost is called");
            return await this.provider.EditPostAsync(postid, username, password, post, publish);
        }

        /// <summary>
        /// delete post as an asynchronous operation.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="postid">The post identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="publish">if set to <c>true</c> the post was marked as published.</param>
        /// <returns>A <see cref="Task{Boolean}" /> indicating whether the post was successfully deleted.</returns>
        [XmlRpcMethod("blogger.deletePost")]
        public async Task<bool> DeletePostAsync(string key, string postid, string username, string password, bool publish)
        {
            this.logger.LogInformation($"MetaWeblog:DeletePost is called");
            return await this.provider.DeletePostAsync(key, postid, username, password, publish);
        }

        /// <summary>
        /// get categories as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task" /> yielding an array of <see cref="CategoryInfo" />.</returns>
        [XmlRpcMethod("metaWeblog.getCategories")]
        public async Task<CategoryInfo[]> GetCategoriesAsync(string blogid, string username, string password)
        {
            this.logger.LogInformation($"MetaWeblog:GetCategories is called");
            return await this.provider.GetCategoriesAsync(blogid, username, password);
        }

        /// <summary>
        /// new media object as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="mediaObject">The media object.</param>
        /// <returns>A <see cref="Task{MediaObjectInfo}" />.</returns>
        [XmlRpcMethod("metaWeblog.newMediaObject")]
        public async Task<MediaObjectInfo> NewMediaObjectAsync(string blogid, string username, string password, MediaObject mediaObject)
        {
            this.logger.LogInformation($"MetaWeblog:NewMediaObject is called");
            return await this.provider.NewMediaObjectAsync(blogid, username, password, mediaObject);
        }

        /// <summary>
        /// get page as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="pageid">The page identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task{Page}" />.</returns>
        [XmlRpcMethod("wp.getPage")]
        public async Task<Page> GetPageAsync(string blogid, string pageid, string username, string password)
        {
            this.logger.LogInformation($"wp.getPage is called");
            return await this.provider.GetPageAsync(blogid, pageid, username, password);
        }

        /// <summary>
        /// get pages as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="numPages">The number pages.</param>
        /// <returns>A <see cref="Task" /> yielding an array of <see cref="Page" />.</returns>
        [XmlRpcMethod("wp.getPages")]
        public async Task<Page[]> GetPagesAsync(string blogid, string username, string password, int numPages)
        {
            this.logger.LogInformation($"wp.getPages is called");
            return await this.provider.GetPagesAsync(blogid, username, password, numPages);
        }

        /// <summary>
        /// get authors as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="Task" /> yielding an array of <see cref="Author" />.</returns>
        [XmlRpcMethod("wp.getAuthors")]
        public async Task<Author[]> GetAuthorsAsync(string blogid, string username, string password)
        {
            this.logger.LogInformation($"wp.getAuthors is called");
            return await this.provider.GetAuthorsAsync(blogid, username, password);
        }

        /// <summary>
        /// add page as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="page">The page.</param>
        /// <param name="publish">if set to <c>true</c> the page will be marked as published.</param>
        /// <returns>A <see cref="Task{String}" />.</returns>
        [XmlRpcMethod("wp.newPage")]
        public async Task<string> AddPageAsync(string blogid, string username, string password, Page page, bool publish)
        {
            this.logger.LogInformation($"wp.newPage is called");
            return await this.provider.AddPageAsync(blogid, username, password, page, publish);
        }

        /// <summary>
        /// edit page as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="pageid">The page identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="page">The page.</param>
        /// <param name="publish">if set to <c>true</c> the page will be marked as published.</param>
        /// <returns>A <see cref="Task{Boolean}" /> indicating whether the page was successfully edited.</returns>
        [XmlRpcMethod("wp.editPage")]
        public async Task<bool> EditPageAsync(string blogid, string pageid, string username, string password, Page page, bool publish)
        {
            this.logger.LogInformation($"wp.editPage is called");
            return await this.provider.EditPageAsync(blogid, pageid, username, password, page, publish);
        }

        /// <summary>
        /// delete page as an asynchronous operation.
        /// </summary>
        /// <param name="blogid">The blog identifier.</param>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="pageid">The page identifier.</param>
        /// <returns>A <see cref="Task{Boolean}" /> indicating whether the page was successfully deleted.</returns>
        [XmlRpcMethod("wp.deletePage")]
        public async Task<bool> DeletePageAsync(string blogid, string username, string password, string pageid)
        {
            this.logger.LogInformation($"wp.deletePage is called");
            return await this.provider.DeletePageAsync(blogid, username, password, pageid);
        }
    }
}
