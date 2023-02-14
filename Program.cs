using System;

namespace LinearEquationsSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the coefficients of the linear equations (enter END when finished):");


            List<double[]> equations_coeff = new List<double[]>();
            int equations_degree = -1;

            while (true)
            {
                Console.WriteLine("Enter the coefficients of the next equation:");

                string input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }

                string[] inputs = input.Split();
                if (equations_degree == -1)
                {
                    equations_degree = inputs.Length - 1;
                }
                else if (inputs.Length - 1 != equations_degree)
                {
                    Console.WriteLine("Error: number of variables does not match previous equations");
                    return;
                }

                double[] equation = new double[inputs.Length];
                for (int i = 0; i < inputs.Length; i++)
                {
                    equation[i] = double.Parse(inputs[i]);
                }

                equations_coeff.Add(equation);
            }

            int number_of_equations = equations_coeff.Count;
            if (number_of_equations != equations_degree)
            {
                Console.WriteLine("Error: number of equations does not match number of variables");
                return;
            }

            Console.WriteLine("The system of linear equations before elimination is:");
            for (int i = 0; i < number_of_equations; i++)
            {
                Console.Write("Equation {0}: ", i + 1);
                for (int j = 0; j < equations_degree; j++)
                {
                    Console.Write("{0}x{1} + ", equations_coeff[i][j], j + 1);
                }
                Console.WriteLine("= {0}", equations_coeff[i][equations_degree]);
            }

            for (int i = 0; i < equations_degree - 1; i++)
            {
                int maxRow = i;
                for (int j = i + 1; j < number_of_equations; j++)
                {
                    if (Math.Abs(equations_coeff[j][i]) > Math.Abs(equations_coeff[maxRow][i]))
                    {
                        maxRow = j;
                    }
                }

                if (equations_coeff[maxRow][i] == 0)
                {
                    Console.WriteLine("The system has no unique solution.");
                    return;
                }

                if (maxRow != i)
                {
                    for (int k = i; k < equations_degree + 1; k++)
                    {
                        double temp = equations_coeff[maxRow][k];
                        equations_coeff[maxRow][k] = equations_coeff[i][k];
                        equations_coeff[i][k] = temp;
                    }
                }

                for (int j = i + 1; j < number_of_equations; j++)
                {
                    double k = equations_coeff[j][i] / equations_coeff[i][i];
                    for (int p = i; p < equations_degree + 1; p++)
                    {
                        equations_coeff[j][p] = equations_coeff[j][p] - k * equations_coeff[i][p];
                    }
                }
            }

            double[] solutions = new double[equations_degree];
            for (int i = equations_degree - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < equations_degree; j++)
                {
                    sum += solutions[j] * equations_coeff[i][j];
                }
                solutions[i] = (equations_coeff[i][equations_degree] - sum) / equations_coeff[i][i];
            }

            Console.WriteLine("The solutions to the system of linear equations are:");
            for (int i = 0; i < equations_degree; i++)
            {
                Console.WriteLine("x{0} = {1}", i + 1, solutions[i]);
            }
        }
    }
}
