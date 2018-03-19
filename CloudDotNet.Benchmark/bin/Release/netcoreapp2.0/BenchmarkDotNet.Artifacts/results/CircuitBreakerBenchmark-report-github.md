``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Intel Core i7-4700MQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical cores and 4 physical cores
Frequency=2338349 Hz, Resolution=427.6522 ns, Timer=TSC
.NET Core SDK=2.1.102
  [Host]     : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT


```
|     Method |         Mean |        Error |        StdDev |        StdErr |       Median |          Min |           Q1 |           Q3 |          Max |           P0 |          P25 |          P50 |          P80 |          P85 |          P90 |          P95 |         P100 |        Op/s |   Scaled | ScaledSD |
|----------- |-------------:|-------------:|--------------:|--------------:|-------------:|-------------:|-------------:|-------------:|-------------:|-------------:|-------------:|-------------:|-------------:|-------------:|-------------:|-------------:|-------------:|------------:|---------:|---------:|
| Sequential |     366.5 ns |     3.138 ns |      2.935 ns |     0.7578 ns |     367.6 ns |     360.9 ns |     363.7 ns |     369.2 ns |     370.1 ns |     360.9 ns |     363.7 ns |     367.6 ns |     369.2 ns |     369.2 ns |     369.4 ns |     369.7 ns |     370.1 ns | 2,728,314.2 |     1.00 |     0.00 |
|   Parallel | 441,072.9 ns | 9,166.391 ns | 27,027.310 ns | 2,702.7310 ns | 450,197.1 ns | 380,999.8 ns | 421,206.4 ns | 456,912.1 ns | 509,598.0 ns | 380,999.8 ns | 421,549.9 ns | 450,197.1 ns | 461,129.9 ns | 464,543.6 ns | 469,426.4 ns | 471,945.2 ns | 509,598.0 ns |     2,267.2 | 1,203.46 |    73.97 |
