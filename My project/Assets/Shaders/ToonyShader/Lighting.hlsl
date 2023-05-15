//    Copyright (C) 2020 NedMakesGames

//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program. If not, see <https://www.gnu.org/licenses/>.

// This is the Lighting.hlsl file for the end of the first video in 2020.2

#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void CalculateMainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out half DistanceAtten, out half ShadowAtten) {
#ifdef SHADERGRAPH_PREVIEW
    Direction = half3(0, 0, 0);
    Color = 1;
    DistanceAtten = 1;
    ShadowAtten = 1;
#else
#if SHADOWS_SCREEN
    half4 clipPos = TransformWorldToHClip(WorldPos);
    half4 shadowCoord = ComputeScreenPos(clipPos);
#else
    half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowAtten = mainLight.shadowAttenuation;
#endif
}

float GetLightIntensity(float3 color){
    return max(color.r, max(color.g, color.b));
}

void AddAdditionalLights_float(float3 WorldPosition, float3 WorldNormal,
    float MainDiffuse, float3 mainColor,
    out float Diffuse, out float3 Color, out float3 LightMap) {
    Diffuse = MainDiffuse;
    Color = mainColor;
    LightMap = float3(0,0,0);

#ifndef SHADERGRAPH_PREVIEW
    float highestDiffuse = Diffuse;

    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i) {
        Light light = GetAdditionalLight(i, WorldPosition, half4(1,1,1,1));
        half NdotL = saturate(dot(WorldNormal, light.direction));
        half atten = light.distanceAttenuation * light.shadowAttenuation * GetLightIntensity(light.color);
        half thisDiffuse = atten * NdotL;
        Diffuse += thisDiffuse;
        //Diffuse = light.shadowAttenuation;
        
        

        if(thisDiffuse > highestDiffuse){
            highestDiffuse = thisDiffuse;
            LightMap = thisDiffuse > 0.1 ? float3(1,1,1) : float3(0,0,0);
            Color = light.color * thisDiffuse;
        }        
    }
#endif
    //Color = Color == float3(1,1,1) ? float3(0,0,0) : Color;
}

#endif
