# MetaWeblog

A C# implementation of the MetaWeblog API for ASP.NET Core partially based on Shawn Wildermuth's implementation.

[![Build status](https://dev.azure.com/improvgroup/MetaWeblog/_apis/build/status/MetaWeblog-ASP.NET%20Core-CI)](https://dev.azure.com/improvgroup/MetaWeblog/_build/latest?definitionId=32)
![](https://vsrm.dev.azure.com/improvgroup/_apis/public/Release/badge/4f5f5de5-83a0-4499-bf9e-f3e652fcb795/1/1)

Shawn's implementation can be found here: https://github.com/shawnwildermuth/MetaWeblog

To use this you need to implement the IMetaWeblogProvider interface...

```C#
public class MyMetaWeblogService : IMetaWeblogProvider
{
    public async Task<UserInfo> GetUserInfoAsync(string key, string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<BlogInfo[]> GetUsersBlogsAsync(string key, string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<Post> GetPostAsync(string postid, string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<Post[]> GetRecentPostsAsync(string blogid, string username, string password, int numberOfPosts)
    {
        throw new NotImplementedException();
    }
    
    public async Task<string> AddPostAsync(string blogid, string username, string password, Post post, bool publish)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeletePostAsync(string key, string postid, string username, string password, bool publish)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> EditPostAsync(string postid, string username, string password, Post post, bool publish)
    {
        throw new NotImplementedException();
    }

    public async Task<CategoryInfo[]> GetCategoriesAsync(string blogid, string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<MediaObjectInfo> NewMediaObjectAsync(string blogid, string username, string password, MediaObject mediaObject)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddCategoryAsync(string key, string username, string password, NewCategory category)
    {
        throw new NotImplementedException();
    }

    public Page GetPage(string blogid, string pageid, string username, string password)
    {
        throw new NotImplementedException();
    }

    public Page[] GetPages(string blogid, string username, string password, int numPages)
    {
        throw new NotImplementedException();
    }

    public Author[] GetAuthors(string blogid, string username, string password)
    {
        throw new NotImplementedException();
    }

    public string AddPage(string blogid, string username, string password, Page page, bool publish)
    {
        throw new NotImplementedException();
    }

    public bool EditPage(string blogid, string pageid, string username, string password, Page page, bool publish)
    {
        throw new NotImplementedException();
    }

    public bool DeletePage(string blogid, string username, string password, string pageid)
    {
        throw new NotImplementedException();
    }
  }
```
  
Then register the service and use it in your Startup.cs file:
  
```C#
    public void ConfigureServices(IServiceCollection svcs)
    {
        svcs.AddMetaWeblog<MyMetaWeblogService>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseMetaWeblog("/livewriter");
    }
```
