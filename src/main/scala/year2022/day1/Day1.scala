package year2022.day1

import scala.io.Source

object Day1 {
  def part1(input: List[Array[Int]]): Int = {
    val list = input.map(_.sum).sorted.reverse
    val result = list.head
    result
  }

  def part2(input: List[Array[Int]]): Int = {
    val list = input.map(_.sum).sorted.reverse
    val result = list.take(3).sum
    result
  }


  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .split("\r\n\r\n")
      .toList
      .map(_.split("\r\n").map(_.toInt))

    println(part1(input))
    println(part2(input))
  }
}
