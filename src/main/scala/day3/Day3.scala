package day3

import day2.Day2.{part1, testData, transformInput}

import scala.io.Source

object Day3 extends App {
  val testData = Source.fromResource("day3/testdata.txt").getLines.toList.map(_.split("").map(_.toInt).toList)
  val data = Source.fromResource("day3/data.txt").getLines.toList.map(_.split("").map(_.toInt).toList)

  part1(data)
  part2(data)

  def part1(input: List[List[Int]]): Unit = println {
    val binary = input
      .transpose
      .map(x => x.groupBy(identity).mapValues(_.size).maxBy(_._2)._1)

    val res1 = binary.mkString("")

    val res2 = binary.map {
      case 1 => 0
      case 0 => 1
    }.mkString("")

    val test = Integer.parseInt(res1, 2)
    val test2 = Integer.parseInt(res2, 2)

    s"res1: ${test * test2}"
  }

  def part2(input: List[List[Int]]): Unit = println{
    val oxygenGeneratorRatingBinary = input.transpose.foldLeft((input, 0))({
      case ((input, iteration), _) =>
        if (input.length == 1) (input, 0) else {
          val transposedInput = input.transpose
          val entriesCount = transposedInput(iteration).groupBy(identity).mapValues(_.size)
          val isEqualEntries = entriesCount.values.forall(_ == entriesCount.head._2)
          val mostCommon = if (isEqualEntries) 1 else entriesCount.maxBy(_._2)._1
          val newList = input.filter(x => x(iteration) == mostCommon)
          (newList, iteration + 1)
        }
    })._1.map(x => x.mkString("")).head

    val co2ScrubberRatingBinary = input.transpose.foldLeft((input, 0))({
      case ((input, iteration), _) =>
        if (input.length == 1) (input, 0)
        else {
          val transposedInput = input.transpose
          val entriesCount = transposedInput(iteration).groupBy(identity).mapValues(_.size)
          val isEqualEntries = entriesCount.values.forall(_ == entriesCount.head._2)
          val leastCommon = if (isEqualEntries) 0 else entriesCount.minBy(_._2)._1
          val newList = input.filter(x => x(iteration) == leastCommon)
          (newList, iteration + 1)
        }
    })._1.map(x => x.mkString("")).head

    val oxygenGeneratorRating = Integer.parseInt(oxygenGeneratorRatingBinary, 2)
    val co2ScrubberRating = Integer.parseInt(co2ScrubberRatingBinary, 2)

    s"res2: ${oxygenGeneratorRating * co2ScrubberRating}"
  }
}
