# LoopDelay.NET

[![Nuget](https://img.shields.io/nuget/v/LoopDelay.NET)](https://www.nuget.org/packages/LoopDelay.NET)

Allows you to add a Delay and Expire time to Loop Conditions, Preventing infinite loops and removing the delay from the body.

# Simple Delay (Faster)
        
| Parameter     | Type          | Usage           |
| ------------- | ------------- | ----------------|
| startTicks | long | (UTC) The time the loop started as Ticks, There are 10,000 ticks in a millisecond|
| delayMs | int | How long the loop should be delayed for in Milliseconds|
| expireMs | int | When the loop should expire in Milliseconds|
        
```cs
    var startTicks = DateTime.UtcNow.Ticks;
    while (await Loop.Delay(startTicks, delayMs, expireMs))
    {
        // Only Your Code Inside the Loop
        break;                   
    }
```

# Strongly Typed Delay (Slower)

| Parameter     | Type          | Usage           |
| ------------- | ------------- | ----------------|
startTime | DateTime |  (UTC) The time the loop started
delayTimeSpan | TimeSpan | How long the loop should be delayed for as a TimeSpan
expireTimeSpan | TimeSpan | When the loop should expire as a TimeSpan
```cs
    var startTime = DateTime.UtcNow;
    while (await Loop.Delay(startTime, delayTimeSpan, expireTimeSpan))
    {
        // Only Your Code Inside the Loop
        break;   
    }
```

# While Conditions

You can add a condition to the while statement like you usually would or you can put it in the body; or both.

```cs
    var startTime = DateTime.UtcNow;
    while (Condition && await Loop.Delay(startTime, delayTimeSpan, expireTimeSpan))
    {
        if(Condition){break;}

        // Only Your Code Inside the Loop
        break;   
    }
```

# Await or .Result()

```cs
 while (await Loop.Delay(startTime, delayTimeSpan, expireTimeSpan))
```

or 

```cs
 while (Loop.Delay(startTime, delayTimeSpan, expireTimeSpan).Result)
```


# OnExpireAction

There is also a version of these that follow the same pattern that trigger an `Action` when the Loop Expires

| Parameter     | Type          | Usage           |
| ------------- | ------------- | ----------------|
OnExpireAction | Action | Action to Run as a Task when the Loop Expires

# Notes

Start time/ticks should use `DateTime.UtcNow`
