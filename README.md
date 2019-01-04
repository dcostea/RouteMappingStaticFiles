# RouteMappingStaticFiles

In an MVC application, the routing is responsible for finding the matching action inside the controller. For this, a series of services are loaded by services.AddMvc alone, like AddApiExplorer, AddAuthorization, AddViews, AddRazorPages, AddFormatterMappings, AddJsonFormatters, AddDataAnnotations, AddCacheTagHelper, AddJsonFormatters, AddCors and more. Take a look inside MvcServiceCollectionExtensions.cs https://github.com/aspnet/Mvc/blob/master/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs.

But what if we need to get a response as quick as possible? All these services included with AddMvc are useless and expensive in terms of performance.

Instead of AddMvc, we can use AddMvcCore or, even less, AddRouting with RouteBuilder and MapGet (MapPost, MapDelete). Of course, this is not what you need for a complex application with multiple controllers and actions.

When sending a rich text response to the client, html code is what you will need (usually an html static file).

Add services.AddRouting() to the ConfigureServices method.

Add next lines to the Configure method:

var routeBuilder = new RouteBuilder(app);
routeBuilder.MapGet("index", async context => {
    await StaticFiles.ReturnStaticFile(context);
});
app.UseRouter(routeBuilder.Build());

RouteBuilder class will allow you to build custom routes and handle them.

MapGet, MapPost (but not limited to these two) methods can be used to build responses for custom routes and, instead of returning some short html code, an entire html file is loaded from wwwroot folder.
