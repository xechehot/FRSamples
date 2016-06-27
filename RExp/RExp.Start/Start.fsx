#I "../packages/RProvider.1.1.20"
#I "../packages/FSharp.Data.2.3.1"
#r "../packages/FSharp.Data.2.3.1/lib/net40/FSharp.Data.dll"
#load "RProvider.fsx"

open System
open RDotNet
open RProvider
open RProvider.graphics
open RProvider.``base``
open RProvider.ggvis
open RProvider.magrittr
open FSharp.Data

let wb = WorldBankData.GetDataContext()
let years = wb.Countries.``Russian Federation``.Indicators.``Population, total``.Years
            |> Seq.filter (fun x-> x>=1960)
let populations = years |> Seq.map (fun y -> wb.Countries.``Russian Federation``.Indicators.``Population, total``.[y])
R.plot(years,populations)
R.title(main="Widgets", xlab="Period", ylab="Quantity")
wb.Topics.Health.Description

(* 1. sepal length in cm
   2. sepal width in cm
   3. petal length in cm
   4. petal width in cm
   5. class: 
      -- Iris Setosa
      -- Iris Versicolour
      -- Iris Virginica *)
type IrisClass = 
    |Setosa
    |Versicolour
    |Virginica
    
type Iris = CsvProvider<"http://archive.ics.uci.edu/ml/machine-learning-databases/iris/iris.data", HasHeaders = false, Schema = "SepalLength(float),SepalWidth(float),PetalLength(float),PetalWidth(float),Name">
let iris = Iris.Load("http://archive.ics.uci.edu/ml/machine-learning-databases/iris/iris.data")
let first = iris.Rows |> Seq.head

let sl = iris.Rows |> Seq.map(fun r->r.SepalLength) 
let sw = iris.Rows |> Seq.map(fun r->r.SepalWidth) 
let pl = iris.Rows |> Seq.map(fun r->r.PetalLength)
let pw = iris.Rows |> Seq.map(fun r->r.PetalWidth)
namedParams [
    "x", box sl;
    "y", box sw;
    "col", box "1"]    
|> R.plot
namedParams [
    "new", box "T"]
|> R.par
namedParams [
    "x", box pl;
    "y", box pw;
    "col", box "2"]    
|> R.plot
