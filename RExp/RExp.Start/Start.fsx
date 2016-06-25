#I "../packages/RProvider.1.1.20"
#I "../packages/FSharp.Data.2.3.1"
#r "../packages/FSharp.Data.2.3.1/lib/net40/FSharp.Data.dll"
#load "RProvider.fsx"

open System
open RDotNet
open RProvider
open RProvider.graphics
open RProvider.``base``
open FSharp.Data

let wb = WorldBankData.GetDataContext()
let years = wb.Countries.``Russian Federation``.Indicators.``Population, total``.Years
            |> Seq.filter (fun x-> x>=1960)
let populations = years |> Seq.map (fun y -> wb.Countries.``Russian Federation``.Indicators.``Population, total``.[y])
R.plot(years,populations)
R.title(main="Widgets", xlab="Period", ylab="Quantity")