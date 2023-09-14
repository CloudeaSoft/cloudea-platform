# cloudea-platform

# 在这里随便记点东西，没啥营养

Modules：使用反射特性，扫描ModuleInitializer类的实现类，调用其方法注入
Logger：Serilog.AspNetCore
Swagger：注释基于asp.netcore的xml接口文档生成
ORM：FreeSql
传输实体包装：Result、PageRequest、PageResponse

后续任务：
1. 简单包装FreeSql的方法，以在必要时可以更方便的更换ORM
2. 重新设计返回结果实体类
3. 尝试重构部分代码，并优化项目结构