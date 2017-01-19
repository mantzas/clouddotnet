``` ini

BenchmarkDotNet=v0.10.1, OS=Windows
Processor=?, ProcessorCount=8
Frequency=2338339 Hz, Resolution=427.6540 ns, Timer=TSC
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Allocated=39 B  

```
                     Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |       Op/s | Scaled | Scaled-StdDev |  Gen 0 |
--------------------------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |----------- |------- |-------------- |------- |
 'Empty pool Rent - Return' | 179.6480 ns | 1.5763 ns | 0.4213 ns | 177.9565 ns | 178.5501 ns | 179.1588 ns | 180.6911 ns | 183.4361 ns | 177.9565 ns | 178.6426 ns | 179.1588 ns | 180.8100 ns | 181.0294 ns | 181.5621 ns | 182.3778 ns | 183.4361 ns | 5566442.44 |   1.00 |          0.00 | 0.0058 |
  'Full pool Rent - Return' | 161.7964 ns | 1.7874 ns | 0.5160 ns | 159.2665 ns | 160.6128 ns | 161.6030 ns | 162.5829 ns | 165.7538 ns | 159.2665 ns | 160.6741 ns | 161.6030 ns | 162.9373 ns | 163.4524 ns | 163.8905 ns | 164.7728 ns | 165.7538 ns | 6180608.92 |   0.90 |          0.01 | 0.0057 |
