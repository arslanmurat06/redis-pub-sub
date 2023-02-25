# redis-pub-sub
Producer Subscriber by using Redis channel

By using redis channel we can create producer-subscriber application. But need to know that redis channel communication
is not reliable. So to make more reliable pub-sub applications queue (Rabbit MQ etc) structures should be considered.


To run Redis on your local machine on Docker:
```
docker run --name redis-pub-sub -p 6379:6379 -d redis
```


and to run background services run on both project (pub,sub):

```
dotnet run
```
