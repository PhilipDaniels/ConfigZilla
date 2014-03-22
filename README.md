# ConfigZilla
*.config file processing for .Net in a simple, scalable style.


Tired of twiddling .config settings in Visual Studio? Annoyed by the
limitations of web.config transforms? ConfigZilla is for you. It gives you the
ability to:

* Centralise all settings and their values in a single project called "ConfigZilla".
  Write custom classes ("MyAppSettings") in here and then reference ConfigZilla
  from your solution's other projects.
* Use MSBuild properties such as $(Configuration) to determine variable values
  in a very DRY-ish way.
* Use arbitrary XSLT transforms to generate your web.config, app.config or
  whatever.config from xxx.template.config.
* Automatically apply those transforms to **any** config file located **anywhere**
  in **any** project in your solution. So you can use the same transforms
  against your website's web.config and a class library's or test project's
  app.config, PLUS with a bit of XSLT magic you can apply the transforms to
  settings stored in the main .config file or in separate files such as
  appSettings.config.
* In other words, all the transforms for your solution are localised to one
  project, the ConfigZilla project, rather than being scattered through all your
  projects.
* Works with F5 debugging.
* Easily handle projects that have partial config requirements. Have 10
  connection strings in your application, but a certain project only uses 3?
  No problem, just define the settings for all 10 in one place and when you
  apply to the 3-project it will only apply the transforms that match.
* Add a comment to your .config files describing when and who they were
  generated by, including the Git commit and branch if you have Git.
* A GUI for quick-and-easy encrypting and decrypting of config sections.

The key questions I asked myself when designing ConfigZilla was "will it
scale to 100 connection strings in 100 projects with 100 configurations?" -
I think it will.


# Dependencies
ConfigZilla requires MSBuild v4.0, which shipped with Visual Studio 2010
and Microsoft .Net 4.0. Apart from that it is completely self-contained.


# Getting Started
Clone this repo and check out the Samples. The project has a website at
http://philipdaniels.com/configzilla which contains discussions and
walkthroughs of the examples and some advanced techniques.

Install ConfigZilla into your solution using NuGet. A project called ConfigZilla
will be created. Peruse the examples in the Transforms folder and adjust them
to meet your needs.

* *.targets files are used to define variable values using standard MSBuild syntax.
* *.xslt files define the XSL transformations to apply to your .template.config files.
  By default, files named *.czDefaults.xslt are always included irrespective of
  your current build configuration. Files named *.$(Configuration).xslt are only
  included if your build configuration matches.

When you compile the ConfigZilla project it creates a file called ConfigZilla.xslt
in the output folder. This contains all the *.xslt transformations, merged
together and with variable substitution performed. Then, when you compile the
other projects in your solution, this file is used to create *.config from
*.template.config.

Lastly, jig up your source control: if you are generating a config file from a
template then do not add that config file to source control. Be careful with
web apps, they can have web.configs in subfolders so just adding "web.config"
to .gitignore is probably too permissive.


# Encryption and Decryption
The ConfigZilla repository contains a tool called "ConfigZilla.Encrypter". This is
a simple GUI program which makes it very easy to encrypt and decrypt your .config
files. Note that you must do this as a post-install step (i.e. you must do it on
the machine that the website or program will be running on) because it uses the
machine's key to do the encryption. ConfigZilla.Encrypter has a command line
mode which allows such tasks to be easily scripted.


# TODO
- [ ] In the Encrypter, remember the last 10 files/sections.
- [ ] In the Encrypter, support DPAPI.
