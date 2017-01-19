``` ini

BenchmarkDotNet=v0.10.1, OS=Linux
Processor=?, ProcessorCount=8
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Allocated=39 B  

```
                     Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |       Op/s | Scaled | Scaled-StdDev |  Gen 0 |
--------------------------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |----------- |------- |-------------- |------- |
 'Empty pool Rent - Return' | 206.0995 ns | 1.6747 ns | 0.4324 ns | 203.4180 ns | 205.1304 ns | 205.7007 ns | 207.4740 ns | 210.1606 ns | 203.4180 ns | 205.1568 ns | 205.7007 ns | 207.5028 ns | 207.6035 ns | 208.1014 ns | 208.9448 ns | 210.1606 ns | 4852024.64 |   1.00 |          0.00 | 0.0010 |
  'Full pool Rent - Return' | 173.6144 ns | 4.4574 ns | 1.1509 ns | 168.3205 ns | 169.5337 ns | 173.1499 ns | 176.8734 ns | 183.7644 ns | 168.3205 ns | 169.5806 ns | 173.1499 ns | 177.0065 ns | 177.4720 ns | 178.0531 ns | 180.0066 ns | 183.7644 ns | 5759889.51 |   0.84 |          0.02 | 0.0062 |
