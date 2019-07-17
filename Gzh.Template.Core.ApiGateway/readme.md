# ocelot网关

文档：https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html

参考：https://www.cnblogs.com/jesse2013/p/net-core-apigateway-ocelot-docs.html

网关的主要功能：路由、负载均衡、服务发现、服务聚合、认证、鉴权、限流、熔断、缓存、header信息传递等等


## 这是一个完整的路由配置：
```
{
    "DownstreamPathTemplate": "/",
    "UpstreamPathTemplate": "/",
    "UpstreamHttpMethod": [
        "Get"
    ],
    "AddHeadersToRequest": {},
    "AddClaimsToRequest": {},
    "RouteClaimsRequirement": {},
    "AddQueriesToRequest": {},
    "RequestIdKey": "",
    "FileCacheOptions": {
        "TtlSeconds": 0,
        "Region": ""
    },
    "ReRouteIsCaseSensitive": false,
    "ServiceName": "",
    "DownstreamScheme": "http",
    "DownstreamHostAndPorts": [
        {
            "Host": "localhost",
            "Port": 51876,
        }
    ],
    "QoSOptions": {
        "ExceptionsAllowedBeforeBreaking": 0,
        "DurationOfBreak": 0,
        "TimeoutValue": 0
    },
    "LoadBalancer": "",
    "RateLimitOptions": {
        "ClientWhitelist": [],
        "EnableRateLimiting": false,
        "Period": "",
        "PeriodTimespan": 0,
        "Limit": 0
    },
    "AuthenticationOptions": {
        "AuthenticationProviderKey": "",
        "AllowedScopes": []
    },
    "HttpHandlerOptions": {
        "AllowAutoRedirect": true,
        "UseCookieContainer": true,
        "UseTracing": true
    },
    "DangerousAcceptAnyServerCertificateValidator": false
}

```
* Downstream是下游服务配置
* UpStream是上游服务配置
* Aggregates 服务聚合配置
* ServiceName, LoadBalancer, UseServiceDiscovery 配置服务发现
* AuthenticationOptions 配置服务认证
* RouteClaimsRequirement 配置Claims鉴权
* RateLimitOptions为限流配置
* FileCacheOptions 缓存配置
* QosOptions 服务质量与熔断
* DownstreamHeaderTransform头信息转发


### 路由模板,转发所有请求：
```
{
    "DownstreamPathTemplate": "/{url}",
    "DownstreamScheme": "https",
    "DownstreamHostAndPorts": [
            {
                "Host": "localhost",
                "Port": 80,
            }
        ],
    "UpstreamPathTemplate": "/{url}",
    "UpstreamHttpMethod": [ "Get" ]
}

```

### 负载均衡：
```
{
    "DownstreamPathTemplate": "/api/posts/{postId}",
    "DownstreamScheme": "https",
    "DownstreamHostAndPorts": [
            {
                "Host": "10.0.1.10",
                "Port": 5000,
            },
            {
                "Host": "10.0.1.11",
                "Port": 5000,
            }
        ],
    "UpstreamPathTemplate": "/posts/{postId}",
    "LoadBalancer": "LeastConnection",
    "UpstreamHttpMethod": [ "Put", "Delete" ]
}

```
LoadBalancer将决定负载均衡的算法
* LeastConnection – 将请求发往最空闲的那个服务器
* RoundRobin – 轮流发送
* NoLoadBalance – 总是发往第一个请求或者是服务发现


