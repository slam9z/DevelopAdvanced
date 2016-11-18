[Why should we use master page when we have Iframes?](http://stackoverflow.com/questions/5113249/why-should-we-use-master-page-when-we-have-iframes)

## answer

You are right, iframes are put extra load on the server and master pages are more efficient. But there are some cases where iframes are necessary. One of the biggest uses for iframes is to display content that is hosted on separate domain. For example, facebook apps are often displayed in iframes so that their content can be hosted on a domain.

Some legacy applications use frames extensively as a design model and include JavaScript to allow frames to communicate. This is generally considered bad design and can often be replaced with master pages in a way that is much easier to understand.