[Vue.js 常见问题 FAQ](http://www.html-js.com/article/1840)


## Vue.js 为何不支持 IE8？

Vue.js 内部使用 ES5 的 `Object.defineProperty` 来转化对象属性为 getter 和 setter，并在 getter 和 setter 中 
emit 事件来实现对对象属性变化的观察。这是 Vue.js 简洁的语法和强劲的性能的基础。IE8 的该方法不能作用于 JS 对象，
也没有办法模拟。

## Vue.js 和 Backbone 有什么区别？

1.  Vue.js 和 Backbone 在定位上不同。Backbone 提供了 Router, View, Model 和 Collection，而 Vue.js 只专注于 
View 这一块。

2.  Backbone 的 Model 在原生数据对象上加了一层抽象，需要用 `get` 和 `set` 来触发事件，而在 Vue.js 里面，JS 
对象即 Model, 数组就是 Collection，不需要学习额外的 API。

3.  Vue.js 自动将数据和 View 绑定，自动跟踪依赖，几乎可以让手动的 DOM 操作绝迹；这让代码由数据驱动，逻辑清晰。
Backbone 完全没有数据绑定，需要手动添加事件回调来操作 DOM，复杂的视图逻辑很快就会难以维护。

## Vue.js 和 AngularJS 有什么区别？

Vue.js 向 Angular 学习了很多优秀的特性，最重要的就是用指令 (Directive) 来扩展 HTML 的语法，并实现 DOM
 操作和数据逻辑的分离。但同时 Vue.js 也有以下优势：

1.  Vue.js 更轻巧，API 更友好，不强迫你所有的代码都遵循 Angular 的各种规定，使用场景更加灵活。

2.  Vue.js 比 Angular 简单易学得多。

3.  Vue.js 不依赖 dirty checking，性能比 Angular 高，在移动应用中尤其明显。

4.  Vue.js 的组件 (Component) 和指令 (Direcitve) 概念区分比较明确。指令只管数据绑定和 DOM 操作，而组件则是
一个包含模版、数据逻辑、私有部件的独立单元。Angular 里两者之间的界限比较模糊。

当然，Vue.js 目前还是个新生项目，而 Angular 的生态系统更为成熟，本身也经历过足够的生产环境的考验，测试相关的工具
也更加完善。

## Vue.js 和 KnockoutJS 有什么区别？

最明显的区别就是 KO 所有的可观测属性都必须手动用 `ko.observable() `来初始化，并且需要用函数调用的方式来操作数据
。相比之下，Vue.js 里拿到 JSON 数据直接就可以作为可观测模型，和操作原生对象没有区别。另外，KO 组合嵌套 ViewModel 
比较复杂，也没有 ViewModel 之间作用域的继承。总的来说，Vue.js 通过只支持现代浏览器获得了更简洁的语法，并加入了
更完善的组件机制。

## Vue.js 和 React.js 有什么区别？

React 在定位上和 Vue.js 很相近：都是专注于 View 层面，并且都是组件化的理念。但内部实现上两者有很大区别。
React 链接数据的视图的方式是在内存中模拟 DOM ，在数据变化后比较分析两次渲染的不同，最后将变化施加在实际的 DOM 上。
而 Vue.js 则是将实际的 DOM 作为模版，数据直接绑定到具体的 DOM node 上，当数据变化的时候只需要对绑定的 node 
进行精确的操作即可。两者孰优孰劣很难说，要视具体使用场景而定。Vue.js 相对 React 有一个优点就是 Directive 的可扩展性
，通过自定义 Directive，开发者可以在需要的时候对 DOM 进行复杂的操作，并且保持这些操作和数据逻辑的分离；而在 React 中，
由于 HTML 是直接通过 JSX 格式包含在 JavaScript 代码中的，视图和逻辑很容易结合得非常紧密，个人觉得并不利于维护。
