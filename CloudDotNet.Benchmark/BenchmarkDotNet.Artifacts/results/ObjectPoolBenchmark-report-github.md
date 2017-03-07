``` ini

BenchmarkDotNet=v0.10.3.0, OS=Microsoft Windows 10.0.14393
Processor=Intel(R) Core(TM) i7-4700MQ CPU 2.40GHz, ProcessorCount=8
Frequency=2338350 Hz, Resolution=427.6520 ns, Timer=TSC
dotnet cli version=1.0.0
  [Host]     : .NET Core 4.6.25009.03, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.25009.03, 64bit RyuJIT


```
 |                     Method |       Mean |    StdDev |    StdErr |        Min |         Q1 |     Median |         Q3 |        Max |         P0 |        P25 |        P50 |        P80 |        P85 |        P90 |        P95 |       P100 |        Op/s | Scaled | Scaled-StdDev |
 |--------------------------- |----------- |---------- |---------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |------------ |------- |-------------- |
 | 'Empty pool Rent - Return' | 70.5469 ns | 0.3238 ns | 0.0836 ns | 69.8554 ns | 70.3361 ns | 70.5630 ns | 70.7455 ns | 71.1830 ns | 69.8554 ns | 70.3434 ns | 70.5630 ns | 70.7459 ns | 70.7473 ns | 70.8525 ns | 71.0006 ns | 71.1830 ns |  14174968.9 |   1.00 |          0.00 |
 |  'Full pool Rent - Return' | 70.9818 ns | 0.4574 ns | 0.1181 ns | 70.1183 ns | 70.7080 ns | 70.8774 ns | 71.3355 ns | 71.8841 ns | 70.1183 ns | 70.7446 ns | 70.8774 ns | 71.3859 ns | 71.5620 ns | 71.6063 ns | 71.6986 ns | 71.8841 ns | 14088111.67 |   1.01 |          0.01 |
