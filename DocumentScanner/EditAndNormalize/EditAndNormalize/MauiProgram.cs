﻿using Microsoft.Extensions.Logging;
using Dynamsoft.CameraEnhancer.Maui;
using Dynamsoft.CameraEnhancer.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;

namespace EditAndNormalize;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
                        .ConfigureLifecycleEvents(events =>
                        {
#if ANDROID
                            events.AddAndroid(android => android
                                .OnResume((activity) =>
                                {
                                    CameraPage.enhancer?.Open();

                                })
                );
#endif
                        })
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(CameraView), typeof(CameraViewHandler));
                handlers.AddHandler(typeof(ImageEditorView), typeof(ImageEditorViewHandler));
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

