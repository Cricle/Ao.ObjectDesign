<div align='center' >
<h1>ObjectDesign</h1>
</div>

<div align='center' >
	<h5>Auto generate object proxy visitor</h5>
</div>

<div align='center'>

[![codecov](https://codecov.io/gh/Cricle/Ao.ObjectDesign/branch/master/graph/badge.svg?token=jyQaSxhCz2)](https://codecov.io/gh/Cricle/Ao.ObjectDesign)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/54617be52d464795bddb2af0c91eadc3)](https://www.codacy.com/gh/Cricle/Ao.ObjectDesign/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Cricle/Ao.ObjectDesign&amp;utm_campaign=Badge_Grade)

</div>

# Language

[简体中文](README.md) [English](README.EN-US.md)

# Build Status

|Provider|Status|
|:-:|:-|
|Github|[![.NET](https://github.com/Cricle/Ao.ObjectDesign/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/Cricle/Ao.ObjectDesign/actions/workflows/dotnet.yml)|
|Azure Pipline|[![Build Status](https://hcricle.visualstudio.com/Ao.ObjectDesign/_apis/build/status/Ao.ObjectDesign?branchName=master)](https://hcricle.visualstudio.com/Ao.ObjectDesign/_build/latest?definitionId=10&branchName=master)|


# What is this 

It provider a way to dynamic proxy object, and then you can visitor them at anywhere, such as geneate design ui, to provide use design.

# How to use

## Proxy object

```csharp

//To generate object proxy.
var proxy=ObjectDesigner.Instance.CreateProxy(obj,obj.GetType());
//Get this layout property proxies
var propProxies=proxy.GetPropertyProxies()

```

To see sample `ObjectDesignRW`

## Generate wpf design ui

You can use type `ForViewBuilder<TView, TContext>`, the project `Ao.ObjectDesign.Wpf` support direct generate ui and `DataTemplateSelector` way to create ui by proxy data.

The project `Ao.ObjectDesign.Wpf` support manay wpf component designer, such as `Point`, `Rect`, `Thickness`...

The project `Ao.ObjectDesign.Controls` support any wpf controls settings, such as `Button`, `CheckBox`, `TextBox`...

If you do not want make all properties proxy and generate ui, you can design yourself type.

If you want save/load the settings, you can use project `Ao.ObjectDesign.Wpf.Json`.

To see sample `ObjectDesign.Wpf`

## Next

- [ ] Add more unit tests
- [ ] Add step recorder, add sequencer