FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY Gzh.Template.Core.sln .
COPY Gzh.Template.Core.Application/Gzh.Template.Core.Application.csproj Gzh.Template.Core.Application/Gzh.Template.Core.Application.csproj
COPY Gzh.Template.Core.Repository/Gzh.Template.Core.Repository.csproj Gzh.Template.Core.Repository/Gzh.Template.Core.Repository.csproj
COPY Gzh.Template.Core.Infrastructure/Gzh.Template.Core.Infrastructure.csproj Gzh.Template.Core.Infrastructure/Gzh.Template.Core.Infrastructure.csproj
COPY Gzh.Template.Core.WebApi/Gzh.Template.Core.WebApi.csproj Gzh.Template.Core.WebApi/Gzh.Template.Core.WebApi.csproj

COPY . ./

RUN dotnet restore

RUN dotnet publish Gzh.Template.Core.WebApi -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app

# 修改容器时区
RUN cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime

COPY --from=build-env /app/Gzh.Template.Core.WebApi/out .
COPY --from=build-env /app/Gzh.Template.Core.WebApi/*.xml .
ENTRYPOINT ["dotnet", "Gzh.Template.Core.WebApi.dll"]