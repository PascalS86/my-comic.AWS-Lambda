# AWS LAMBDA FOR CsandraComics

This AWS lambda function allows us to access an AWS dynamoDB data store. It handles a get and post method send to an API Gateway, which is defined by the *server.template* file. 

This project was created with dotnet CLI from AWS.

## Here are some steps to follow from Visual Studio:

To deploy your Serverless application, right click the project in Solution Explorer and select *Publish to AWS Lambda*.

To view your deployed application open the Stack View window by double-clicking the stack name shown beneath the AWS CloudFormation node in the AWS Explorer tree. The Stack View also displays the root URL to your published application.

## Here are some steps to follow to get started from the command line:

Once you have edited your template and code you can deploy your application using the [Amazon.Lambda.Tools Global Tool](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) from the command line.

Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

Execute unit tests
```
    cd "aws.lambda.core/test/aws.lambda.core.Tests"
    dotnet test
```

Deploy application
```
    cd "BlueprintBaseName/src/BlueprintBaseName"
    dotnet lambda deploy-serverless
```
