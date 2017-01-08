``` ini

BenchmarkDotNet=v0.10.1, OS=Windows
Processor=?, ProcessorCount=8
Frequency=2338334 Hz, Resolution=427.6549 ns, Timer=TSC
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Allocated=39 B  

```
                     Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |       Op/s | Scaled | Scaled-StdDev |  Gen 0 |
--------------------------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |----------- |------- |-------------- |------- |
 'Empty pool Rent - Return' | 132.9515 ns | 0.3797 ns | 0.0980 ns | 132.4367 ns | 132.6579 ns | 132.8465 ns | 133.2862 ns | 133.8301 ns | 132.4367 ns | 132.7108 ns | 132.8465 ns | 133.2952 ns | 133.3266 ns | 133.3542 ns | 133.5077 ns | 133.8301 ns | 7521541.27 |   1.00 |          0.00 | 0.0064 |
  'Full pool Rent - Return' | 103.8508 ns | 0.2352 ns | 0.0607 ns | 103.6101 ns | 103.6739 ns | 103.7377 ns | 104.0936 ns | 104.4046 ns | 103.6101 ns | 103.6866 ns | 103.7377 ns | 104.1007 ns | 104.1257 ns | 104.1316 ns | 104.2146 ns | 104.4046 ns |  9629201.9 |   0.78 |          0.00 | 0.0062 |
