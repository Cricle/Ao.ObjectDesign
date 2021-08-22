<div align='center' >
<h1>对象设计器</h1>
</div>

<div align='center' >
	<h5>自动化对象设计</h5>
</div>

<div align='center'>

[![codecov](https://codecov.io/gh/Cricle/Ao.ObjectDesign/branch/master/graph/badge.svg?token=jyQaSxhCz2)](https://codecov.io/gh/Cricle/Ao.ObjectDesign)[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FCricle%2FAo.ObjectDesign.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FCricle%2FAo.ObjectDesign?ref=badge_shield)

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/54617be52d464795bddb2af0c91eadc3)](https://www.codacy.com/gh/Cricle/Ao.ObjectDesign/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Cricle/Ao.ObjectDesign&amp;utm_campaign=Badge_Grade)

</div>

# 语言

[简体中文](README.md) [English](README.EN-US.md)


# 构建状态

|Provider|Status|
|:-:|:-|
|Github|[![.NET](https://github.com/Cricle/Ao.ObjectDesign/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/Cricle/Ao.ObjectDesign/actions/workflows/dotnet.yml)|
|Azure Pipline|[![Build Status](https://hcricle.visualstudio.com/Ao.ObjectDesign/_apis/build/status/Ao.ObjectDesign?branchName=master)](https://hcricle.visualstudio.com/Ao.ObjectDesign/_build/latest?definitionId=10&branchName=master)|


# 这是什么 

这提供了一种动态代理对象技术，并且提供了另一种方式去访问对象，所以能使用此方式去生成设计UI提供给用户去设计

# 如何使用

## 代理对象

```csharp

//生成代理对象
var proxy=ObjectDesigner.Instance.CreateProxy(obj,obj.GetType());
//获取当前代理层的代理属性集合
var propProxies=proxy.GetPropertyProxies()

```

示例可见 `ObjectDesignRW`

## 生成WPF设计UI

你能使用类型`ForViewBuilder<TView, TContext>`, 工程`Ao.ObjectDesign.Wpf` 提供了直接生成UI的方式和使用`DataTemplateSelector`通过代理对象生成ui的方式。

工程 `Ao.ObjectDesign.Wpf`提供了很多wpf的组件设计器，例如`Point`, `Rect`, `Thickness`...

工程`Ao.ObjectDesign.Controls`提供了几乎全部的wpf基础控件，例如`Button`, `CheckBox`, `TextBox`...

如果你不想让设计器覆盖所以属性并生成，你可以制作自己独有的设计器。

让一个你想保存/加载设计结果，你可以使用工程`Ao.ObjectDesign.Wpf.Json`

示例可见`ObjectDesign.Wpf`

## 下一步

- [ ] 添加更多的单元测试
- [ ] 提供步骤记录器，提供顺序器

## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FCricle%2FAo.ObjectDesign.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FCricle%2FAo.ObjectDesign?ref=badge_large)