[Airbnb CSS-in-JavaScript Style Guide] (https://github.com/airbnb/javascript/tree/master/css-in-javascript)


##Table of Contents

1. [Naming](#naming)
1. [Ordering](#ordering)
1. [Nesting](#nesting)
1. [Inline](#inline)
1. [Themes](#themes)

##Naming

    - Use camelCase for object keys (i.e. "selectors").
    
    - Use an underscore for modifiers to other styles.
    
    - Use selectorName_fallback for sets of fallback styles.
    
    - Use a separate selector for sets of fallback styles.
    >Why? Keeping fallback styles contained in a separate object clarifies their purpose, which improves readability.

    - Use device-agnostic names (e.g. "small", "medium", and "large") to name media query breakpoints


