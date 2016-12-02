[Introduction to Effects](http://learn.jquery.com/effects/intro-to-effects/)


##Showing and Hiding Content

jQuery can show or hide content instantaneously with .show() or .hide():


##Fade and Slide Animations

You may have noticed that .show() and .hide() use a combination of slide and fade effects when 
showing and hiding content in an animated way. If you would rather show or hide content with 
one effect or the other, there are additional methods that can help. .slideDown() and .slideUp()
show and hide content, respectively, using only a slide effect. Slide animations are accomplished
by rapidly making changes to an element's CSS height property.


##Changing Display Based on Current Visibility State

jQuery can also let you change a content's visibility based on its current visibility state.
 .toggle() will show content that is currently hidden and hide content that is currently visible.


##Doing Something After an Animation Completes

A common mistake when implementing jQuery effects is assuming that the execution of the next method 
in your chain will wait until the animation runs to completion.


##Managing Animation Effects

jQuery provides some additional features for controlling your animations:


###.stop()

###.delay()

###jQuery.fx