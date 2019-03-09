# ✨dotnet-imgup [imgup]

.NET Core global CLI tool to upload images on [imgur](https://imgur.com/)

## Install

The `dotnet-imgup` nuget package is [published to nuget.org](https://www.nuget.org/packages/dotnet-imgup/)

You can get the tool by running this command

`$ dotnet tool install -g dotnet-imgup`

## Usage

    Usage: imgup [options]

    Options:
        help          Display help
        upload        Upload images to imgur
        list          List of all uploaded images
        delete        Delete images from imgur
        clear         Clear uploads history

    Ex:
        imgup upload ./flower.jpg ../img/table.jpg

        flower.jpg uploaded to http://i.imgur.com/orunSTu.jpg
        table.jpg uploaded to http://i.imgur.com/itaoTyS.jpg

    Ex:
        imgup delete TRB7Naih22ilflc QZBbj6QnYlpIVXp

        deleted TRB7Naih22ilflc
        deleted QZBbj6QnYlpIVXp
