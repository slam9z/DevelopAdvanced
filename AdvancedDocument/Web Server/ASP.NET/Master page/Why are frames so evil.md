[Why are frames so evil?](http://www.html-faq.com/htmlframes/?framesareevil)



# Why are frames so evil?
	

Frames are evil. Frames supposedly make the webdesigners job easier, but they cause an increased maintenance overhead. Frames supposedly creates a better interface to a website for the end-user, but they cause severe usability problems.

Its common to see frames abused by newbies in implementing a left-hand menu and top banner layout with the mistaken belief its easier to maintain and makes downloading quicker. There are numerous problems this implementation raises typically related to the paradox it creates.

To make-up for the usability deficiencies, many framed websites use some client-side techniques which cause further maintenance nightmares. There is a definite usability versus maintenance trade-off with frames, which make it a difficult technology to manage well. The alternatives available have none of these drawbacks, thus frames are a sub-optimal, and typically backward solution.

Most of this "usability"-hacking of framed websites results in a complete dependancy on Javascript - another evil. Considering the on-going problems related to Windows lax security model (in the OS, Outlook and Internet Explorer) and the exponential growth of scripted worms and viruses (Melissa, Love Bug, Kornikova, SirCam, Code Red, Code Red II, Code Blue, Nimda), this convinces a greater number of surfers switching off Javascript entirely, which in turn causes a framed and scripted site to die a rather horrible death in the browser.

The perceived benefits of frames are:

*   Quicker to download, since the menu and banner don't have to be reloaded each time.
*   Easier to maintain because the content is separate from the layout and navigation.
*   More usable since the menu is always visible on screen

## The Web Paradox of Frames

The web was built as a network of addressable resources (typically of information) where each resource can be identified by a unique URL. Frames breaks this architecture because now many resources are grouped together under one URL (the frameset URL), but only one resource can be shown (typically the default page defined in the frameset).

## Bookmarking

When bookmarking a page, visitors expect to bookmark the exact page of information, but the frameset gets bookmarked instead so when users follow this bookmark they then have to hunt for the relevant page every time. This is the drawback of breaking the Web guideline of a one-to-one mapping between URLs and resources.

_"Also, you're right. It is annoying when you try to bookmark and its always the original frameset page. I got around this by having each section of site being its own frameset, so you can bookmark each section and go back to it."_ - the author here now needs to create a frameset for every potential combination of pages, typically one frameset per page of content. There goes both the quicker to download advantage and the easier to maintain advantage.

## Search engines

Search engines have problems with framed websites because of the frames paradox. This results in unframed pages being indexed and linked to, so typically the user sees the first page without the frameset, and typically without navigation (known as blackhole pages). By adding navigation links to the content, the developer has lost the advantage of separating content from presentation and navigation.

_"You can get around search engine problems by creating a doorway page full of your keywords, and then redirecting to the frameset"_ - Going from the frying pan into the fire, this technique pushes all search engine traffic to doorway pages, effectively duplicating the content. The visitor is then expected to manually surf through the frameset finding what they were supposed to get to from the search engine. Not a good visitor experience plus the overhead of duplicating maintenance. If you are going to create doorway pages, then it makes a lot of sense to put the actual content there and forget about the frames entirely.

_"You can redirect unframed pages to pages within frames using Javascript"_ - Of course, now usability and accessibiliy is being forced entirely into a reliance on Javascript.

## Content and Presentation Separation

The issue of framed websites are easier to maintain is ludicrous, since to keep up with non-framed sites the developer will have to duplicate the menu (something frames was supposed to prevent), then creating framesets for every combination of content/banner/navigation so the pages are bookmarkable (thus losing out on the maintainability argument). Since the reason for using frames in this instance was to only have one copy of the navigation bar. You'll need to have another copy for the &lt;noframes&gt; tag in every frameset - else the website will be up there with the 100,000+ other people under "This site uses frames" keyword search.

## Printing

Frames break the standard browser print buttons. So what gets printed is typically not the page the user wants printed, because of the seemingly random ways which browsers select which frame to send to the printer. It typically prints the frame that has the focus, but without frame borders (which are typically removed by newbie frames authors), the visual cues of the selected page are hidden, thus affecting usability of the website.

## Usability

The appearance of multiple pairs of scrollbars affects usability, since users are essentially confused as to which scrollbars to use. Hiding these scrollbars has the effect of hiding all content that doesn't fit into the frame, thus seriously impacts users finding their way around a website or finding the content they wanted.

## Download times

If you "need" frames to cut down the load time you need to re-think how you're presenting the content. Images, style-sheets and Javascript should be cached.

## Linking and copyright

Framing third-party information into another web page raises issues of copyright infringement (derivative works), passing off, defamation, and trademark infringement -- [BitLaw: Linking](http://www.bitlaw.com/internet/linking.html)

## The alternatives

SSI, CGI, PHP, ASP do a far superior job of including one menu into all content pages and don't annoy the site visitor. It all comes down to usability for the visitor and convenience for the site developer. Using frames involves a trade-off of at least one of these factors. Where these server-side tools are not available, the web developer can always fall-back to the html-preprocessor, which is a "cheap man's SSI".

When designing a website, the primary rule of development is "Only Trust the Server". The website developer has the choice, thus control, over the server where the website will be kept. So the developer should always trust this environment to deliver the content as required.

From the visitor's point of view frames degrade their experience, they make it difficult to find what they are looking for, and when the visitor eventually finds it, they can't bookmark, or print successfully. From the web developer's point of view, frames are supposedly easier to maintain - but looking at the hacks above to get around search engine problems and bookmarking problems, the effort required to maintain the website looks to be almost double a non-framed website (or four times the effort if there is a non-framed version).

* * *

There are, however, good uses for frames. But as a cheap replacement for server side tools and html-preprocessors, they are inadequate and lacking. The type of applications that frames are adequately capable of handling are those applications that don't require bookmarking, don't require search-engine indexing (and positively discourage it), and don't require the ability to be accessible to the World Wide Web. These typically are work-flow based applications that are created for a specific purpose, and not for the general Internet population.

