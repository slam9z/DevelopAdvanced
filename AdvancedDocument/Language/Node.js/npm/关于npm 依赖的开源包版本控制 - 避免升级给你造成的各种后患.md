[关于npm 依赖的开源包版本控制 - 避免升级给你造成的各种后患](http://yijiebuyi.com/blog/e928200b81775f18a587b4150514b3cb.html)


你有没有认认真真写过package.json 文件?

其中 package.json 里的依赖配置项是非常重要的.

```json
"dependencies": {
    "MD5": "^1.2.1",
    "async": "^0.9.0",
    "body-parser": "^1.9.2",
    "cluster": ">= 0.7.7",
    "commander": "^2.6.0",
    "compression": "^1.2.0",
    "config": "^1.7.0",
    "connect-multiparty": "^1.2.5",
    "crypto": "0.0.3",
    "domain-middleware": "^0.1.0",
    "express": ">= 4.0.0",
    "express-domain-middleware": "^0.1.0"
}
```

上面是我从package.json 里随便截取一部分依赖配置.

你能看到关于版本的控制出现各种符号,我们稍后一一说明.


开发过程中有一种开发人员确实会认认真真的写依赖配置,但是往往在版本控制处写了 "*"  

当你的项目版本以来配置项里出现了星号,一定要注意了.这样的控制几乎可以说等于没有,以后的维护,发布一旦遇上高版本,可能兼容性瞬间奔溃.


虽然作为一个开源开发者对于自己的版本升级本着向下兼容的原则,但是实际上并不是这样的.


node.js 项目并不是编译一次到处执行,不同环境下都需要在各自平台编译.所以如果你给同事/同学拷贝项目的时候,如果你们用不同的系统,都需要在各自系统上重新编译.

这时package.json 配置里的依赖可以帮助我们下载到指定版本的包.只需要下面一个命令

```
npm install
```

插入一下:

npm 是node.js 里的包管理器,是一个命令行工具,想了解它的使用请输入

```
npm help
```

它不需要单独安装,在我们安装node.js的时候,已经自动安装了 npm

我们常用的npm 命令就是 install

```
npm install gulp --save
```

就像上面的命令一样,当你安装的时候带上了 --save ,npm会自动在你命令所在目录里找到 package.json 文件,并追加到依赖配置最后一样.

当我不加任何版本安装时,默认追加方式是这样的:

```
"gulp": "^3.8.9"
```

关于gulp构建请看此文


npm也可以安装时指定一个版本

npm install express@4.0.1


如果你不知道此依赖包共有哪些版本,可以通过 info 命令来查看

```
zhangzhi@moke:~/code/blog2014$ npm info md5
npm http GET https://registry.npmjs.org/md5
npm http 200 https://registry.npmjs.org/md5
```

```json

{
	name: 'md5',
	description: 'Kohyama\'s jsMD5. See http://jsperf.com/md5-shootout. I\'ll replace this on npm with anything else.',
	'dist-tags': {
		latest: '1.0.0'
	},
	versions: ['1.0.0', '1.0.1'],
	maintainers: 'coolaj86 <coolaj86@gmail.com>',
	time: {
		modified: '2015-01-10T04:35:43.515Z',
		created: '2011-08-27T21:25:23.732Z',
		'1.0.0': '2012-05-13T00:00:30.935Z',
		'1.0.1': '2012-05-13T00:00:11.449Z'
	},
	author: 'Yoshinori Kohyama (http://jp.linkedin.com/pub/yoshinori-kohyama/36/155/9b8)',
	repository: {
		type: 'git',
		url: 'git://github.com/coolaj86/jsMD5.git'
	},
	users: {
		haeck: true,
		samhou1988: true
	},
	homepage: 'https://github.com/coolaj86/jsMD5',
	keywords: ['ender', 'md5', 'browser'],
	readmeFilename: '',
	version: '1.0.0',
	main: 'index.js',
	engines: {
		node: '*'
	},
	dependencies: {},
	devDependencies: {},
	optionalDependencies: {},
	dist: {
		shasum: '9f50a07f110b70e0cf38ecf4423c3b1b7e82605d',
		tarball: 'http://registry.npmjs.org/md5/-/md5-1.0.0.tgz'
	},
	directories: {}
}
```

它帮我们列出了有关开源包的所有信息.


关于npm 的详细信息可以单独开一篇博客来讲了,我们还是要回到 package.json 的版本控制:

```
"cluster": ">= 0.7.7",
"commander": "^2.6.0",
"async" : "*",
```

我们来看下上面这3种有何区别:


### 第一种:

貌似通过字面意思你应该懂得, cluster 版本必须大于等于 0.7.7

npm 安装的时候你也可以这样指定:

npm install cluster@">=0.7.7"

甚至你可以把版本范围指定到更小

npm install cluster@">=0.7.7<0.8.0"

让安装的版本大于0.7.7并且小于 0.8.0


### 第二种:

上面我们提到过默认通过包名称安装,写入package.json 里的版本控制前面就加了 ^ (上尖号)

^ 符号表示,可以接受 小版本和补丁版本的变化.什么意思?简单说就是大版本不变即可,其他版本随便更新.

"commander":"^2.6.0"  

当我们npm install 的时候,安装到 node_modules 目录下的 commander开源包可能是 2.6.0 或 2.7.3 或 2.8.9 ......只要前面2不变即可.

这种版本限制先对宽松,还是少用为妙.

如果你确定代码已经健壮,api已经非常稳定,而且开发者确实对兼容性做的很好,关键开发者还符合版本规范,这样控制版本也可行.


### 第三种:

"async": " * "

我上面说过的,最糟糕的一种版本控制,一点限制没有.

假如:

我本地使用了 async.forEach 接口,代码都测试没有问题,过了一段时间准备发布到服务器上,拷贝代码,运行 npm install 都没有问题.

但就是程序报错跑不起来. 因为 不存在 async.forEach 这个方法.(好像是变成了 async.each 方法,不太确定了)

去本地调试,确实有啊,怎么回事, 这就是典型的版本迭代更换api方法名导致的错误,叫天天不灵,叫地地不灵.

所以防患于未然,我们不要使用 " * " 来做版本控制.


也许有人说了,那我怎么知道该使用哪个版本号? 

上面介绍了一种 npm info [包名称] 来查看包的所有信息 (版本都列出来了)

你需要哪个版本,去开源代码托管处查看api 说明,确定好你代码使用的版本.


如果你是一个很懒的人,当时做项目的时候,就是参考 github 上开源代码的api来实现的,那你开源直接用npm 通过 github 开源地址来安装依赖包.

```
npm install git://github.com/package/path.git
```

早晨写了一个版本,结果没有保存误点了浏览器后退按钮,郁闷了一上午,中午补上,当时已经完全和上午的版本不一样.