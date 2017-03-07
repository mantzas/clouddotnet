``` ini

BenchmarkDotNet=v0.10.1, OS=Linux
Processor=?, ProcessorCount=8
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Allocated=159 B  

```
                     Method |       Mean |    StdDev |    StdErr |        Min |         Q1 |     Median |         Q3 |        Max |         P0 |        P25 |        P50 |        P80 |        P85 |        P90 |        P95 |       P100 |        Op/s | Scaled | Scaled-StdDev |  Gen 0 |
--------------------------- |----------- |---------- |---------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |----------- |------------ |------- |-------------- |------- |
 'Empty pool Rent - Return' | 75.9680 ns | 1.6518 ns | 0.4768 ns | 75.1281 ns | 75.2264 ns | 75.3783 ns | 75.8876 ns | 81.0575 ns | 75.1281 ns | 75.2361 ns | 75.3783 ns | 76.0711 ns | 76.2642 ns | 76.3753 ns | 78.4934 ns | 81.0575 ns | 13163431.02 |   1.00 |          0.00 | 0.0475 |
  'Full pool Rent - Return' | 74.5349 ns | 0.2706 ns | 0.0699 ns | 74.2004 ns | 74.2849 ns | 74.4834 ns | 74.7661 ns | 75.0512 ns | 74.2004 ns | 74.3212 ns | 74.4834 ns | 74.7787 ns | 74.8231 ns | 74.9146 ns | 74.9953 ns | 75.0512 ns | 13416533.21 |   0.98 |          0.02 | 0.0479 |
s | 5759889.51 |   0.84 |          0.02 | 0.0062 |
