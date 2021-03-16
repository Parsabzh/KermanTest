#pragma checksum "C:\KermanCraft\kermancraft\KermanCraft.Web\Areas\Admin\Views\Admin\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f387574f117398b33c6bbb5cc9e1f3d969e225dc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Admin_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Admin/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\KermanCraft\kermancraft\KermanCraft.Web\Areas\Admin\Views\_ViewImports.cshtml"
using KermanCraft.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\KermanCraft\kermancraft\KermanCraft.Web\Areas\Admin\Views\_ViewImports.cshtml"
using KermanCraft.Domain.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f387574f117398b33c6bbb5cc9e1f3d969e225dc", @"/Areas/Admin/Views/Admin/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"06fa53d569a72b552a97dfc6bcdc86f0666136e5", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Admin_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<KermanCraft.Application.ViewModels.Admin.AdminIndex>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\KermanCraft\kermancraft\KermanCraft.Web\Areas\Admin\Views\Admin\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-lg-3 col-6\">\r\n        <div class=\"stat-box bg-darkblue shadow\">\r\n            <a href=\"#\">\r\n                <div class=\"stat\">\r\n                    <div class=\"counter-down\" data-value=\"");
#nullable restore
#line 11 "C:\KermanCraft\kermancraft\KermanCraft.Web\Areas\Admin\Views\Admin\Index.cshtml"
                                                     Write(Model.ArtistCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""></div>
                    <div class=""h3"">هنرمندان</div>
                </div><!-- /.stat -->
                <div class=""visual"">
                    <i class=""icon-user""></i>
                </div><!-- /.visual -->
            </a>
        </div><!-- /.stat-box -->
    </div><!-- /.col-lg-3 -->
    <div class=""col-lg-3 col-6"">
        <div class=""stat-box bg-blue shadow"">
            <a href=""#"">
                <div class=""stat"">
                    <div class=""counter-down"" data-value=""");
#nullable restore
#line 24 "C:\KermanCraft\kermancraft\KermanCraft.Web\Areas\Admin\Views\Admin\Index.cshtml"
                                                     Write(Model.CustomerCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""></div>
                    <div class=""h3"">مشتریان</div>
                </div><!-- /.stat -->
                <div class=""visual"">
                    <i class=""fas fa-file-invoice-dollar""></i>
                </div><!-- /.visual -->
            </a>
        </div><!-- /.stat-box -->
    </div><!-- /.col-lg-3 -->
    <div class=""col-lg-3 col-6"">
        <div class=""stat-box bg-orange shadow"">
            <a href=""#"">
                <div class=""stat"">
                    <div class=""counter-down"" data-value=""");
#nullable restore
#line 37 "C:\KermanCraft\kermancraft\KermanCraft.Web\Areas\Admin\Views\Admin\Index.cshtml"
                                                     Write(Model.CompanyCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""></div>
                    <div class=""h3"">شرکت</div>
                </div><!-- /.stat -->
                <div class=""visual"">
                    <i class=""fas fa-receipt""></i>
                </div><!-- /.visual -->
            </a>
        </div><!-- /.stat-box -->
    </div><!-- /.col-lg-3 -->
    <div class=""col-lg-3 col-6"">
        <div class=""stat-box bg-red shadow"">
            <a href=""#"">
                <div class=""stat"">
                    <div class=""counter-down"" data-value=""");
#nullable restore
#line 50 "C:\KermanCraft\kermancraft\KermanCraft.Web\Areas\Admin\Views\Admin\Index.cshtml"
                                                     Write(Model.ProductCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""></div>
                    <div class=""h3"">محصول</div>
                </div><!-- /.stat -->
                <div class=""visual"">
                    <i class=""fas fa-coffee""></i>
                </div><!-- /.visual -->
            </a>
        </div><!-- /.stat-box -->
    </div><!-- /.col-lg-3 -->
</div><!-- /.row -->

");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script src=""/assets/plugins/chart.js/dist/Chart.bundle.min.js""></script>
    <script src=""/assets/plugins/jquery-incremental-counter/jquery.incremental-counter.min.js""></script>
    <script src=""/assets/plugins/ammap3/ammap/ammap.js""></script>
    <script src=""/assets/plugins/ammap3/ammap/maps/js/iranHighFa.js""></script>
    <script src=""/assets/js/pages/dashboard2.js""></script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<KermanCraft.Application.ViewModels.Admin.AdminIndex> Html { get; private set; }
    }
}
#pragma warning restore 1591
