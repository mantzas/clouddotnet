``` ini

BenchmarkDotNet=v0.10.1, OS=Windows
Processor=?, ProcessorCount=8
Frequency=2338334 Hz, Resolution=427.6549 ns, Timer=TSC
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Gen 0=0.1417  Allocated=518 B  

```
  Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |       Op/s |
-------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |----------- |
 Execute | 402.7719 ns | 2.9076 ns | 0.7507 ns | 399.3184 ns | 400.0974 ns | 401.8601 ns | 404.6383 ns | 409.1287 ns | 399.3184 ns | 400.4233 ns | 401.8601 ns | 404.9591 ns | 406.0816 ns | 406.5621 ns | 407.4814 ns | 409.1287 ns | 2482794.96 |
1 |
.06 |
