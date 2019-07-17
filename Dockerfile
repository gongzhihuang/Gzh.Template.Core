FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY Gzh.Template.Core.Application/*.csproj ./Gzh.Template.Core.Application/
COPY Gzh.Template.Core.Repository/*.csproj ./Gzh.Template.Core.Repository/
COPY Gzh.Template.Core.Infrastructure/*.csproj ./Gzh.Template.Core.Infrastructure/
COPY Gzh.Template.Core.WebApi/*.csproj ./Gzh.Template.Core.WebApi/

WORKDIR /app/Gzh.Template.Core.WebApi
RUN dotnet restore

# copy and publish app and libraries
WORKDIR /app/
COPY Gzh.Template.Core.WebApi/. ./Gzh.Template.Core.WebApi/
COPY Gzh.Template.Core.Application/. ./Gzh.Template.Core.Application/
COPY Gzh.Template.Core.Infrastructure/. ./Gzh.Template.Core.Infrastructure/
COPY Gzh.Template.Core.Repository/. ./Gzh.Template.Core.Repository/
WORKDIR /app/Gzh.Template.Core.WebApi
RUN dotnet publish -c Release -o out




FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app

# 修改容器时区
RUN cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime

COPY --from=build /app/Gzh.Template.Core.WebApi/out ./  /app/Gzh.Template.Core.WebApi/*.xml ./
ENTRYPOINT ["dotnet", "Gzh.Template.Core.WebApi.dll"]