``` ini

BenchmarkDotNet=v0.10.6, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4700MQ CPU 2.40GHz (Haswell), ProcessorCount=8
Frequency=2338344 Hz, Resolution=427.6531 ns, Timer=TSC
dotnet cli version=1.0.4
  [Host]     : .NET Core 4.6.25211.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.25211.01, 64bit RyuJIT


```
 |                     Method |     Mean |     Error |    StdDev |    StdErr |      Min |       Q1 |   Median |       Q3 |      Max |       P0 |      P25 |      P50 |      P80 |      P85 |      P90 |      P95 |     P100 |         Op/s | Scaled |
 |--------------------------- |---------:|----------:|----------:|----------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|-------------:|-------:|
 | 'Empty pool Rent - Return' | 67.68 ns | 0.2535 ns | 0.1979 ns | 0.0571 ns | 67.36 ns | 67.56 ns | 67.66 ns | 67.84 ns | 68.00 ns | 67.36 ns | 67.58 ns | 67.66 ns | 67.89 ns | 67.92 ns | 67.92 ns | 67.96 ns | 68.00 ns | 14,774,769.7 |   1.00 |
 |  'Full pool Rent - Return' | 67.94 ns | 0.4093 ns | 0.3418 ns | 0.0948 ns | 67.10 ns | 67.70 ns | 68.04 ns | 68.16 ns | 68.43 ns | 67.10 ns | 67.73 ns | 68.04 ns | 68.16 ns | 68.18 ns | 68.21 ns | 68.30 ns | 68.43 ns | 14,718,261.3 |   1.00 |
