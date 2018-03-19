``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Intel Core i7-4700MQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical cores and 4 physical cores
Frequency=2338349 Hz, Resolution=427.6522 ns, Timer=TSC
.NET Core SDK=2.1.102
  [Host]     : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT


```
|                     Method |     Mean |     Error |    StdDev |    StdErr |      Min |       Q1 |   Median |       Q3 |      Max |       P0 |      P25 |      P50 |      P80 |      P85 |      P90 |      P95 |     P100 |         Op/s | Scaled |
|--------------------------- |---------:|----------:|----------:|----------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|-------------:|-------:|
| &#39;Empty pool Rent - Return&#39; | 66.33 ns | 0.3306 ns | 0.3093 ns | 0.0799 ns | 65.71 ns | 66.15 ns | 66.38 ns | 66.55 ns | 66.74 ns | 65.71 ns | 66.16 ns | 66.38 ns | 66.55 ns | 66.56 ns | 66.60 ns | 66.66 ns | 66.74 ns | 15,075,877.9 |   1.00 |
|  &#39;Full pool Rent - Return&#39; | 67.13 ns | 0.3452 ns | 0.3229 ns | 0.0834 ns | 66.55 ns | 67.00 ns | 67.07 ns | 67.45 ns | 67.60 ns | 66.55 ns | 67.00 ns | 67.07 ns | 67.46 ns | 67.51 ns | 67.54 ns | 67.56 ns | 67.60 ns | 14,895,582.5 |   1.01 |
