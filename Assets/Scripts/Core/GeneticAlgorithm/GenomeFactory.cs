using System.Collections.Generic;

public class GenomeFactory
{
    private const int NUM_INPUT_NEURONS = 6;
    private const int NUM_HIDDEN_LAYERS = 2;
    private const int NUM_NEURONS_PER_HIDDEN_LAYER = 8;
    private const int NUM_OUTPUT_NEURONS = 4;

    public static Genome CreateRandom()
    {
        return new Genome(NUM_INPUT_NEURONS, NUM_HIDDEN_LAYERS, NUM_NEURONS_PER_HIDDEN_LAYER, NUM_OUTPUT_NEURONS);
    }

    public static Genome Create(List<decimal> weights)
    {
        return new Genome(NUM_INPUT_NEURONS, NUM_HIDDEN_LAYERS, NUM_NEURONS_PER_HIDDEN_LAYER, NUM_OUTPUT_NEURONS,
            weights);
    }
}